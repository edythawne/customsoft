
DROP FUNCTION app.get_response;

CREATE OR REPLACE FUNCTION app.get_response(_message TEXT, _data JSONB) RETURNS JSONB LANGUAGE plpgsql AS
$$
DECLARE
    response JSONB;
BEGIN
    -- Construir la respuesta JSON
    response := jsonb_build_object(
        'message', _message,
        'data', _data
    );

    RETURN response;
END;
$$;

-- SELECT app.get_response('Hello', 1::VARCHAR::jsonb);
DROP FUNCTION app.user_store;

CREATE OR REPLACE FUNCTION app.user_store(_data TEXT) RETURNS JSONB LANGUAGE plpgsql AS
$$
DECLARE
    __result INTEGER;
    __id_user INTEGER := 0;
    __user_record RECORD;
BEGIN

    -- Asignar valores a un registro para manipular la informacion
    SELECT * INTO __user_record FROM json_to_record(_data::json) AS json(
        "name" text, "first_name" text, "email" text, "password" TEXT, "fk_department" INTEGER, "fk_rol" INTEGER, "created_by" TEXT
    );

    -- Verificar que el correo no exista con perform
    PERFORM 1 FROM auth.user au WHERE au.email = __user_record.email;

    IF NOT FOUND THEN

         -- Guardar el registro en la tabla de usuarios
        INSERT INTO auth.user(name, last_name, email, password, created_by)
        VALUES (__user_record.name, __user_record.first_name, __user_record.email,  __user_record.password, __user_record.created_by)
        RETURNING id INTO __id_user;

        -- Si se creo el usuario agregarle roles y departamento
        IF __id_user > 0 THEN

            -- Vincular rol
            INSERT INTO auth.role_has_user(role_id, user_id)
            VALUES(__user_record.fk_rol, __id_user)
            RETURNING role_id INTO __result;

            IF __result IS NULL THEN
                RAISE EXCEPTION 'Se ha producido un error al vincular el rol al usuario';
            END IF ;

            -- Vincular departamento
            INSERT INTO app.user_has_department(user_id, department_id, created_by)
            VALUES(__id_user, __user_record.fk_department, __user_record.created_by)
            RETURNING id INTO __result;

            IF __result IS NULL THEN
                RAISE EXCEPTION 'Se ha producido un error al vincular el rol al usuario';
            END IF ;

            RETURN app.get_response('Se insertado correctamente',__id_user::VARCHAR::JSONB);

        END IF;

    END IF;

    -- El correo ya esta siendo ocupado
    RETURN app.get_response('El email proporcionado esta siendo ocuapado', '-1'::jsonb);


EXCEPTION
    WHEN OTHERS THEN
        RETURN app.get_response(SQLERRM, '-2'::jsonb);

END;


$$;

SELECT app.user_store('{ "password" : "XXXX", "name" : "Valentina", "first_name" : "Perez", "email" : "itsramires@gmail.com", "fk_department" : 1, "fk_rol" : 2, "created_by" : 1 }');


DROP FUNCTION auth.user_by_login;

CREATE OR REPLACE FUNCTION auth.user_by_login(_data TEXT) RETURNS JSONB LANGUAGE plpgsql AS
$$
DECLARE
    __subquery JSON;
    __email VARCHAR;
    __password VARCHAR;
    __result JSON;
BEGIN

    -- Convertir el texto a JSON
    __subquery := _data::json;

    -- Extraer los valores del JSON
    __email := (__subquery ->> 'email')::VARCHAR;
    __password := (__subquery ->> 'password')::VARCHAR;

    -- Realizar la consulta y construir el JSON de respuesta
    SELECT json_build_object(
        'id', au.id,
        'name', au.name,
        'last_name', au.last_name,
        'email', au.email,
        'token', au.token,
        'created_at', au.created_at,
        'roles', (
            SELECT json_agg(json_build_object(
                'id', ar.id,
                'name', ar.name
            ))
            FROM auth.roles ar
            INNER JOIN auth.role_has_user arhu ON ar.id = arhu.role_id
            WHERE arhu.user_id = au.id
        ),
        'department', (
            SELECT json_build_object(
                'id', cd.id,
                'area_id', cd.area_id,
                'name', cd.name,
                'created_at', cd.created_at,
                'area', (
                    SELECT json_build_object(
                        'id', ca.id,
                        'name', ca.name,
                        'created_at', ca.created_at
                    )
                    FROM catalog.area ca
                    WHERE ca.id = cd.area_id
                )
            )
            FROM app.user_has_department auhp
            INNER JOIN catalog.department cd ON auhp.department_id = cd.id
            WHERE auhp.user_id = au.id
        )
    ) INTO __result
    FROM auth.user au
    WHERE au.email = __email AND au.password = __password;

    -- Usar app.get_response para devolver la respuesta
    RETURN app.get_response('Ok', __result::JSONB);

END;
$$;


SELECT auth.user_by_login('{ "email" : "edyramper@gmail.com", "password" : "qwertyui" }');


DROP FUNCTION app.user_get_count_paginated;

CREATE OR REPLACE FUNCTION app.user_get_count_paginated(
    _search CHARACTER VARYING DEFAULT NULL,
    _active boolean DEFAULT NULL
) RETURNS JSONB LANGUAGE plpgsql AS
$$
DECLARE
    __count BIGINT := 0;
BEGIN

    SELECT COUNT(au.id) INTO __count FROM auth.user au
    WHERE (_search IS NULL OR CONCAT(au.name, ' ', au.last_name) ILIKE CONCAT('%', _search, '%'));

    RETURN __count;

END;
$$;


DROP FUNCTION app.user_get_all_paginated;
CREATE OR REPLACE FUNCTION app.user_get_all_paginated(
    _search CHARACTER VARYING DEFAULT NULL,
    _active boolean DEFAULT NULL,
    _page INTEGER DEFAULT NULL,
    _limit INTEGER DEFAULT NULL
) RETURNS JSONB LANGUAGE plpgsql AS
$$
DECLARE
    __offset BIGINT := 0;
    __count_paginated BIGINT := 0;
    __json_result JSON;
BEGIN

    -- Obtener el inicio del paginador
    IF _page IS NOT NULL AND _limit IS NOT NULL THEN
        __offset = ((_page - 1) * _limit);

        -- Obtener cuantos registros cumplen la paginacion
        SELECT app.user_get_count_paginated(_search, _active) INTO __count_paginated;
    END IF;

    SELECT json_agg(json) AS data FROM(
        SELECT json_build_object(
            'id', au.id,
            'name', au.name,
            'last_name', au.last_name,
            'email', au.email,
            'token', au.token,
            'created_at', au.created_at,
            'roles', (
                SELECT json_agg(json_build_object(
                    'id', ar.id,
                    'name', ar.name
                ))
                FROM auth.roles ar
                INNER JOIN auth.role_has_user arhu ON ar.id = arhu.role_id
                WHERE arhu.user_id = au.id
            ),
            'department', (
                SELECT json_build_object(
                    'id', cd.id,
                    'area_id', cd.area_id,
                    'name', cd.name,
                    'created_at', cd.created_at,
                    'area', (
                        SELECT json_build_object(
                            'id', ca.id,
                            'name', ca.name,
                            'created_at', ca.created_at
                        )
                        FROM catalog.area ca
                        WHERE ca.id = cd.area_id
                    )
                )
                FROM app.user_has_department auhp
                INNER JOIN catalog.department cd ON auhp.department_id = cd.id
                WHERE auhp.user_id = au.id
            ),
            'detail', (
                SELECT json_build_object(
                    'id', auhd.id,
                    'user_id', auhd.user_id,
                    'curp', auhd.curp,
                    'date_of_birth', auhd.date_of_birth,
                    'place_of_birth', auhd.place_of_birth,
                    'gender', auhd.gender,
                    'birth_certificate', auhd.birth_certificate,
                    'created_at', auhd.created_at
                )
                FROM app.user_has_detail auhd
                WHERE auhd.user_id = au.id
            )
       ) AS json
        FROM auth.user au
        WHERE (_search IS NULL OR CONCAT(au.name, ' ', au.last_name) ILIKE CONCAT('%', _search, '%'))
        GROUP BY au.id
        LIMIT NULLIF(_limit, 0)  OFFSET NULLIF(__offset, 0)
    ) json INTO __json_result;

    -- Construir el json final
    RETURN app.get_response('Ok', json_build_object(
        'count_paginated', __count_paginated,
        'data', COALESCE(__json_result, '[]'::JSON)
    )::jsonb);

END;
$$;


SELECT app.user_get_all_paginated();



DROP FUNCTION app.user_sync_detail;


CREATE OR REPLACE FUNCTION app.user_sync_detail(_data TEXT) RETURNS JSONB LANGUAGE plpgsql AS
$$
DECLARE
    __id_detail INTEGER;
BEGIN

    -- Asignar valores a un registro para manipular la informacion
    WITH subquery AS (
        SELECT * FROM json_to_record(_data::json) AS x(
            "user_id" INTEGER,
            "curp" TEXT,
            "date_of_birth" DATE,
            "place_of_birth" TEXT,
            "birth_certificate" TEXT,
            "gender" TEXT,
            "created_by" TEXT
        )
    )
    SELECT subquery.user_id INTO __id_detail FROM subquery, app.user_has_detail auhd WHERE auhd.user_id = subquery.user_id;

    IF __id_detail IS NULL THEN
        WITH subquery AS (
            SELECT * FROM json_to_record(_data::json) AS x(
                "user_id" INTEGER,
                "curp" TEXT,
                "date_of_birth" DATE,
                "place_of_birth" TEXT,
                "birth_certificate" TEXT,
                "gender" TEXT,
                "created_by" TEXT
            )
        )
        INSERT INTO app.user_has_detail(user_id, curp, date_of_birth, place_of_birth, gender, birth_certificate, created_by)
        SELECT subquery.user_id, subquery.curp, subquery.date_of_birth, subquery.place_of_birth, subquery.gender, subquery.birth_certificate, subquery.created_by
        FROM subquery
        WHERE NOT EXISTS(
            SELECT 1 FROM app.user_has_detail auhd WHERE auhd.user_id = subquery.user_id
        ) RETURNING id INTO __id_detail;

    ELSE
        WITH subquery AS (
            SELECT * FROM json_to_record(_data::json) AS x(
                "user_id" INTEGER,
                "curp" TEXT,
                "date_of_birth" DATE,
                "place_of_birth" TEXT,
                "birth_certificate" TEXT,
                "gender" TEXT,
                "created_by" TEXT
            )
        )
        UPDATE app.user_has_detail auhd
        SET curp = subquery.curp,
            date_of_birth = subquery.date_of_birth,
            place_of_birth = subquery.place_of_birth,
            gender = subquery.gender,
            birth_certificate = subquery.birth_certificate,
            updated_by = subquery.created_by
        FROM subquery
        WHERE auhd.user_id = subquery.user_id;
    END IF;

    RETURN app.get_response('Se ha realizado la acción correctamente', __id_detail::VARCHAR::JSONB);

EXCEPTION
    WHEN OTHERS THEN
        RETURN app.get_response(SQLERRM, '-2'::jsonb);

END;
$$;

-- Llamada de ejemplo a la función:
SELECT app.user_sync_detail('{"user_id" : 1, "curp" : "1", "date_of_birth" : "1996-10-10", "place_of_birth" : "a", "birth_certificate" : "a", "gender" : "M", "created_by" : "1"}');
