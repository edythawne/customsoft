
-- CREATE DATABASE IF NOT EXISTS soft_v1;

/**
 * CustomSoft
 */

ALTER DATABASE soft_v1 SET timezone TO 'America/Mexico_City';


-- CREATE USER soft_admin WITH PASSWORD 'aedc2c55b94d';
-- ALTER USER soft_admin WITH SUPERUSER;


/**
 * DROP SCHEMA IF EXISTS auth CASCADE;
 * DROP SCHEMA IF EXISTS catalog CASCADE;
 * DROP SCHEMA IF EXISTS app CASCADE;
 */

CREATE SCHEMA IF NOT EXISTS auth;

CREATE TABLE IF NOT EXISTS auth.user(
    id SERIAL NOT NULL,
    name VARCHAR(250) NOT NULL,
    last_name VARCHAR(250) NULL,
    email VARCHAR(250) NULL,
    password VARCHAR(250) NULL,
    email_confirm BOOLEAN DEFAULT FALSE NOT NULL,
    token VARCHAR(100) NULL,
    active BOOLEAN DEFAULT TRUE NOT NULL,
    created_by VARCHAR(150) NOT NULL DEFAULT 'sys',
    updated_by VARCHAR(150) NOT NULL DEFAULT 'sys',
    deleted_by VARCHAR(150) NULL,
    created_at TIMESTAMP(0) WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP(0) WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    deleted_at TIMESTAMP(0) WITHOUT TIME ZONE NULL,
    PRIMARY KEY(id)
);

ALTER TABLE auth.user ADD CONSTRAINT unique_user_email UNIQUE (email);



CREATE TABLE IF NOT EXISTS auth.roles (
    id SERIAL NOT NULL,
    name VARCHAR(255) NOT NULL,
    name_clearly VARCHAR(100) NOT NULL,
    guard_name VARCHAR(255) NULL,
    created_by VARCHAR(150) NOT NULL DEFAULT 'sys',
    updated_by VARCHAR(150) NOT NULL DEFAULT 'sys',
    deleted_by VARCHAR(150) NULL,
    created_at TIMESTAMP(0) WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP(0) WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    deleted_at TIMESTAMP(0) WITHOUT TIME ZONE NULL,
    PRIMARY KEY(id)
);


CREATE TABLE IF NOT EXISTS auth.role_has_user (
    role_id INT NOT NULL,
    user_id INT NOT NULL,
    UNIQUE(user_id, role_id)
);


CREATE SCHEMA IF NOT EXISTS catalog;



CREATE TABLE IF NOT EXISTS catalog.area (
    id SERIAL NOT NULL,
    name VARCHAR(100) NOT NULL,
    name_clearly VARCHAR(100) NOT NULL,
    active BOOLEAN DEFAULT TRUE NOT NULL,
    created_by VARCHAR(150) NOT NULL DEFAULT 'sys',
    updated_by VARCHAR(150) NULL,
    deleted_by VARCHAR(150) NULL,
    created_at TIMESTAMP(0) WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP(0) WITHOUT TIME ZONE NULL,
    deleted_at TIMESTAMP(0) WITHOUT TIME ZONE NULL,
    PRIMARY KEY(id)
);


CREATE TABLE IF NOT EXISTS catalog.department (
    id SERIAL NOT NULL,
    area_id INT NOT NULL,
    name VARCHAR(100) NOT NULL,
    name_clearly VARCHAR(100) NOT NULL,
    active BOOLEAN DEFAULT TRUE NOT NULL,
    created_by VARCHAR(150) NOT NULL DEFAULT 'sys',
    updated_by VARCHAR(150) NULL,
    deleted_by VARCHAR(150) NULL,
    created_at TIMESTAMP(0) WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP(0) WITHOUT TIME ZONE NULL,
    deleted_at TIMESTAMP(0) WITHOUT TIME ZONE NULL,
    PRIMARY KEY(id)
);


ALTER TABLE catalog.department ADD FOREIGN KEY (area_id) REFERENCES catalog.area ON DELETE CASCADE;


CREATE SCHEMA IF NOT EXISTS app;


CREATE TABLE IF NOT EXISTS app.user_has_department (
    id SERIAL NOT NULL,
    user_id INT NOT NULL,
    department_id INT NOT NULL,
    active BOOLEAN DEFAULT TRUE NOT NULL,
    created_by VARCHAR(150) NOT NULL DEFAULT 'sys',
    updated_by VARCHAR(150) NULL,
    deleted_by VARCHAR(150) NULL,
    created_at TIMESTAMP(0) WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP(0) WITHOUT TIME ZONE NULL,
    deleted_at TIMESTAMP(0) WITHOUT TIME ZONE NULL,
    PRIMARY KEY(id)
);


ALTER TABLE app.user_has_department ADD FOREIGN KEY (user_id) REFERENCES auth.user ON DELETE CASCADE;
ALTER TABLE app.user_has_department ADD FOREIGN KEY (department_id) REFERENCES catalog.department ON DELETE CASCADE;


CREATE TABLE IF NOT EXISTS app.user_has_detail (
    id SERIAL NOT NULL,
    user_id INT NOT NULL,
    curp VARCHAR(18) NOT NULL,
    date_of_birth DATE NOT NULL,
    place_of_birth VARCHAR(500) NOT NULL,
    gender VARCHAR(1) NOT NULL,
    birth_certificate VARCHAR(500) NOT NULL,
    active BOOLEAN DEFAULT TRUE NOT NULL,
    created_by VARCHAR(150) NOT NULL DEFAULT 'sys',
    updated_by VARCHAR(150) NULL,
    deleted_by VARCHAR(150) NULL,
    created_at TIMESTAMP(0) WITHOUT TIME ZONE NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP(0) WITHOUT TIME ZONE NULL,
    deleted_at TIMESTAMP(0) WITHOUT TIME ZONE NULL,
    PRIMARY KEY(id)
);


ALTER TABLE app.user_has_detail ADD FOREIGN KEY (user_id) REFERENCES auth.user ON DELETE CASCADE;

-- Agregar datos por defecto

INSERT INTO auth.roles(id, name, name_clearly, guard_name)
VALUES (1, 'Administrador', 'administrador', 'web'),
       (2, 'Operador', 'operador', 'web');


INSERT INTO catalog.area(id, name, name_clearly)
VALUES (1, 'Dirección', 'administrativo'),
       (2, 'Administrativo', 'administrativo');


INSERT INTO catalog.department(id, area_id, name, name_clearly)
VALUES (1, 1, 'Dirección General', 'direccion_general'),
       (2, 2, 'Recursos Humanos', 'recursos_humanos'),
       (3, 2, 'Recursos Financieros', 'recursos_financieros');


INSERT INTO auth.user (id, name, last_name, email, password, email_confirm, token, active, created_by)
VALUES (1, 'Eduardo', 'Ramires', 'edyramper@gmail.com', 'qwertyui', true, null, true, 'sys');


INSERT INTO auth.role_has_user(role_id, user_id)
VALUES (1, 1);


INSERT INTO app.user_has_department(id, user_id, department_id)
VALUES (1, 1, 2);



SELECT setval('auth.user_id_seq', (SELECT MAX(id) FROM auth.user) + 1);
SELECT setval('auth.roles_id_seq', (SELECT MAX(id) FROM auth.roles) + 1);

SELECT setval('catalog.area_id_seq', (SELECT MAX(id) FROM catalog.area) + 1);
SELECT setval('catalog.department_id_seq', (SELECT MAX(id) FROM catalog.department) + 1);

SELECT setval('app.user_has_department_id_seq', (SELECT MAX(id) FROM app.user_has_department) + 1);
SELECT setval('app.user_has_detail_id_seq', (SELECT MAX(id) FROM app.user_has_detail) + 1);

