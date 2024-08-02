import moment from 'moment'

export default {

    /**
     * Obtiene una fecha en formato dd/mm/yyyy y regresa 'DD [de] MMMM [del] YYYY'
     * @param date
     */
    fullDateString(date: string): string {
        return moment(date).format('DD [de] MMMM [del] YYYY')
    }

}
