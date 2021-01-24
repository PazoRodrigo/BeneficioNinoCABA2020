var _Lista_TipoBeneficio;
class TipoBeneficio {
    constructor() {
        this.IdEntidad = 0;
        this.Nombre = '';
    }
    static async Todos() {
        if (_Lista_TipoBeneficio == undefined) {
            _Lista_TipoBeneficio = await TipoBeneficio.TraerTodos();
        }
        return _Lista_TipoBeneficio;
    }

    static async TraerUno(IdBuscado) {
        let buscado = $.grep(await TipoBeneficio.Todos(), function (entidad, index) {
            return entidad.IdEntidad == IdBuscado;
        });
        return buscado[0];
    }

    static async TraerTodos() {
        try {
            let entidad = "TipoBeneficios";
            let metodo = "TraerTodos";
            let url = ApiURL + "/" + entidad + "/" + metodo;
            let datos = await TraerAPI(url);
            let result = [];
            $.each(datos, function (key, value) {
                result.push(llenarEntidadTipoBeneficio(value));
            });
            return result;
        } catch (error) {
            throw new Error(error);
        }
    }
}
function llenarEntidadTipoBeneficio(entidad) {
    let objResult = new TipoBeneficio();
    objResult.IdEntidad = entidad.IdEntidad;
    objResult.Nombre = entidad.Nombre;
    return objResult;
}
