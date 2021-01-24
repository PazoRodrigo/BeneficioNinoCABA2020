var _ListaProvincias;

class Provincia {
    constructor() {
        this.IdEntidad = 0;
        this.Nombre = '';
        this.Letra = '';
    }

    // Traer
    static async Todos() {
        if (_ListaProvincias == undefined) {
            _ListaProvincias = await Provincia.TraerTodas();
        }
        _ListaProvincias.sort(SortXNombre);
        return _ListaProvincias;
    }
    static async TraerTodos() {
        return await Provincia.Todos();
    }
    // Traer
    static async TraerUna(IdEntidad) {
        _ListaProvincias = await Provincia.Todos();
        let buscado = $.grep(_ListaProvincias, function (entidad, index) {
            return entidad.IdEntidad == IdEntidad;
        });
        let Encontrado = buscado[0];
        return Encontrado;
    }
    static async TraerTodas() {
        let entidad = 'provincias';
        let metodo = 'TraerTodos';
        let url = ApiURL + '/' + entidad + '/' + metodo;
        let lista = await TraerAPI(url);
        _ListaProvincias = [];
        let result = [];
        $.each(lista, function (key, value) {
            result.push(llenarEntidadProvincia(value));
        });
        _ListaProvincias = result;
        return _ListaProvincias;
    }
    static async Refresh() {
        _ListaProvincias = await Provincia.TraerTodas();
    }
}
function llenarEntidadProvincia(entidad) {
    let obj = new Provincia;
    obj.IdEntidad = entidad.IdEntidad;
    obj.Nombre = entidad.Nombre;
    obj.Letra = entidad.Letra;
    return obj;
}