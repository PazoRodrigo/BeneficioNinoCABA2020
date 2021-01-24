var _Lista_TipoParentesco;
class TipoParentesco {
    constructor() {
        this.IdEntidad = 0;
        this.Nombre = '';
    }
    static async Todos() {
        if (_Lista_TipoParentesco == undefined) {
            _Lista_TipoParentesco = await TipoParentesco.TraerTodos();
        }
        return _Lista_TipoParentesco;
    }

    static async TraerUno(IdBuscado) {
        let buscado = $.grep(await TipoParentesco.Todos(), function (entidad, index) {
            return entidad.IdEntidad == IdBuscado;
        });
        return buscado[0];
    }

    static async TraerTodos() {
        try {
            let entidad = "TipoParentescos";
            let metodo = "TraerTodos";
            let url = ApiURL + "/" + entidad + "/" + metodo;
            let datos = await TraerAPI(url);
            let result = [];
            $.each(datos, function (key, value) {
                result.push(llenarEntidadTipoParentesco(value));
            });
            return result;
        } catch (error) {
            throw new Error(error);
        }
    }
}
function llenarEntidadTipoParentesco(entidad) {
    let objResult = new TipoParentesco();
    objResult.IdEntidad = entidad.IdEntidad;
    objResult.Nombre = entidad.Nombre;
    return objResult;
}
