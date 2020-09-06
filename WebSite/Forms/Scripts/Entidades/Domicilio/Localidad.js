var _ListaLocalidades;

class Localidad {
    constructor() {
        this.IdEntidad = 0;
        this.Nombre = '';
        this.CP = 0;
        this.IdSeccional = 0;
        this.IdProvincia = 0;
    }
    async ObjProvincia() {
        return Provincia.TraerUna(this.IdProvincia);
    }
    // static async TraerUna(IdEntidad) {
    //     let data = {
    //         "IdLocalidad": IdEntidad
    //     };
    //     let entidad = 'localidades';
    //     let metodo = 'TraerUna';
    //     let url = ApiURL + '/' + entidad + '/' + metodo;
    //     let datos = await TraerAPI(url, data);
    //     let result = [];
    //     $.each(datos, function (key, value) {
    //         result.push(llenarEntidadLocalidad(value));
    //     });
    //     return result[0];
    // }
    static async TraerTodosXCodigoPostal(CodigoPostal) {
        let data = {
            "CodigoPostal": CodigoPostal
        };
        let entidad = 'localidades';
        let metodo = 'TraerTodosXCodigoPostal';
        let url = ApiURL + '/' + entidad + '/' + metodo;
        let lista = await TraerAPI(url, data);
        _ListaLocalidades = [];
        let result = [];
        $.each(lista, function (key, value) {
            result.push(llenarEntidadLocalidad(value));
        });
        _ListaLocalidades = result;
        return _ListaLocalidades;
    }
    static async TraerUno(IdEntidad) {
        let buscado = $.grep(_ListaLocalidades, function (entidad, index) {
            return entidad.IdEntidad == IdEntidad;
        });
        return buscado[0];
    }
    static async ArmarCombo(lista, div, IdSelect, selector, evento, estilo) {
        let Cbo = '';
        Cbo += '<select id="' + IdSelect + '"  class="' + estilo + '" onchange="SeleccionLocalidad(this);" data-Id="0" data-Evento="' + evento + '">';
        Cbo += '    <option value="0" id="' + selector + '">Seleccionar ...</option>';
        $(lista).each(function () {
            let sel = '';
            Cbo += '<option ' + sel + ' value="' + this.IdEntidad + '" >' + Left(this.Nombre, 50) + '</option>';
        });
        Cbo += '</select>';
        return $('#' + div + '').html(Cbo);
    }
}

function llenarEntidadLocalidad(entidad) {
    let obj = new Localidad;
    obj.IdEntidad = entidad.IdEntidad;
    obj.Nombre = entidad.Nombre;
    obj.CodigoPostal = entidad.CodigoPostal;
    obj.IdSeccional = entidad.IdSeccional;
    obj.IdProvincia = entidad.IdProvincia;
    return obj;
}
async function SeleccionLocalidad(MiElemento) {
    try {
        let elemento = document.getElementById(MiElemento.id);
        let buscado = $.grep(_ListaLocalidades, function (entidad, index) {
            return entidad.IdEntidad == elemento.options[elemento.selectedIndex].value;
        });
        let Seleccionado = buscado[0];
        let evento = elemento.getAttribute('data-Evento');
        let event = new CustomEvent(evento, { detail: Seleccionado });
        document.dispatchEvent(event);
    } catch (e) {
        alertAlerta(e);
    }
}