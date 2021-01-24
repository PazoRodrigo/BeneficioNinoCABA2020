
class Beneficio {
    constructor() {
        this.IdEntidad = 0;
        this.IdRepresentado = 0;
        this.IdFamiliar = 0;
        this.IdBeneficio = 0;
        this.IdTipoBeneficio = 0;
        this.IdTipoParentesco = 0;
        this.Entregado = 0;
        this.FechaEntrega = 0;
        this.IdSeccional = 0;
        this.NroDocumento = 0;

        this._ObjFamiliar;
    }

    async ObjTitular() {
        if (this._ObjTitular == undefined)
            this._ObjTitular = await Titular.TraerUno(this.IdTitular);
        return this._ObjTitular;
    }
    async ObjFamiliar() {
        if (this._ObjFamiliar == undefined)
            this._ObjFamiliar = await Familiar.TraerUno(this.IdTitular, this.IdFamiliar);
        return this._ObjFamiliar;
    }
    async ObjTipoBeneficio() {
        if (this._ObjTipoBeneficio == undefined)
            this._ObjTipoBeneficio = await TipoBeneficio.TraerUno(this.IdTipoBeneficio);
        return this._ObjTipoBeneficio;
    }

    static async TraerTodosxAfiliado_BeneficioEntrega(idAfiliado) {
        let data = {
            "idAfiliado": idAfiliado
        };
        let entidad = 'Beneficios';
        let metodo = 'TraerTodosxAfiliado_BeneficioEntrega';
        let url = ApiURL + '/' + entidad + '/' + metodo;
        let datos = await TraerAPI(url, data);
        let result = [];
        $.each(datos, function (key, value) {
            result.push(llenarEntidadBeneficio(value));
        });
        return result;
    }
    static async TraerTodosxAfiliado(idTitular) {
        try {
            let data = {
                idAfiliado: idTitular
            };
            let entidad = "Beneficios";
            let metodo = "TraerTodosxAfiliado";
            let url = ApiURL + "/" + entidad + "/" + metodo;
            let datos = await TraerAPI(url, data);
            let result = [];
            $.each(datos, function (key, value) {
                result.push(llenarEntidadBeneficio(value));
            });
            return result;
        } catch (error) {
            throw new Error(error);
        }
    }
    static async ArmarGrilla(div, ListaBeneficios, ObjTitular) {
        $('#' + div + '').html('');
        let str = '';

        if (ListaBeneficios?.length > 0) {
            str += ' <table class="table table-bordered">';
            str += '     <thead>';
            str += '         <tr class="bg-primary text-light">';
            str += '             <th class="text-center">Nombre</th>';
            str += '             <th class="text-center">Parentesco</th>';
            str += '             <th class="text-center">Nro. Documento</th>';
            str += '             <th class="text-center">Edad</th>';
            str += '             <th class="text-center">TIpo Beneficio</th>';
            str += '         </tr>';
            str += '     </thead>';
            str += '     <tbody>';
            let BeneficioTitular = $.grep(ListaBeneficios, function (entidad, index) {
                return entidad.NroDocumento == ObjTitular.NroDocumento;
            });
            let BeneficioFamiliares = $.grep(ListaBeneficios, function (entidad, index) {
                return entidad.NroDocumento != ObjTitular.NroDocumento;
            });
            if (BeneficioTitular?.length == 1) {
                let ObjTipoBeneficio = await TipoBeneficio.TraerUno(BeneficioTitular[0].IdTipoBeneficio);
                str += '    <tr>';
                str += '        <td class="text-left pl-1"> ' + ObjTitular.ApellidoNombre + ' </td>';
                str += '        <td class="text-center"> TITULAR </td>';
                str += '        <td class="text-center"> ' + ObjTitular.NroDocumento + ' </td>';
                str += '        <td class="text-right pr-4"> ' + LongToDateString(ObjTitular.FechaNacimiento) + '</td>';
                str += '        <td class="text-right pr-4"><small>' + ObjTipoBeneficio.Nombre + ' </small></td>';
                str += '    </tr>';
            }
            let i = 0;
            while (i <= BeneficioFamiliares.length - 1) {
                let NroDocumentoFamiliar = BeneficioFamiliares[i].NroDocumento;
                let ObjFamiliar = $.grep(_ListaFamiliares, function (entidad, index) {
                    return entidad.NroDocumento == NroDocumentoFamiliar;
                });
                let Nombre = ObjFamiliar[0].ApellidoNombre;
                let ObjParentesco = await TipoParentesco.TraerUno(BeneficioFamiliares[i].IdTipoParentesco);
                let ObjTipoBeneficio = await TipoBeneficio.TraerUno(BeneficioFamiliares[i].IdTipoBeneficio);

                str += '    <tr>';
                str += '        <td class="text-left pl-1">' + Left(Nombre, 20) + '  </td>';
                str += '        <td class="text-center"> ' + ObjParentesco.Nombre + '</td>';
                str += '        <td class="text-center"> ' + NroDocumentoFamiliar + ' </td>';
                str += '        <td class="text-right pr-4"> ' + LongToDateString(ObjFamiliar[0].FechaNacimiento) + ' </td>';
                str += '        <td class="text-right pr-4"><small>' + ObjTipoBeneficio.Nombre + ' </small></td>';
                str += '    </tr>';
                i++;
            }
        }
        str += '     <t/body>';
        str += ' </table>';
        return $('#' + div + '').html(str);
    }
}
function llenarEntidadBeneficio(entidad) {
    let objResult = new Beneficio();
    objResult.IdEntidad = entidad.IdEntidad;
    objResult.NroDocumento = entidad.NroDocumento;
    objResult.Periodo = entidad.Periodo;
    objResult.IdTitular = entidad.IdRepresentado;
    objResult.IdTipoBeneficio = entidad.IdTipoBeneficio;
    objResult.IdTipoParentesco = entidad.IdTipoParentesco;
    objResult.Entregado = entidad.Entregado;
    objResult.FechaEntrega = entidad.FechaEntrega;
    objResult.IdSeccional = entidad.IdSeccional;

    objResult._ObjFamiliar = entidad.ObjFamiliar;
    return objResult;
}
class EntregaBeneficio {
    constructor() {
        this.IdEntidad = 0;
        this.IdTitular = 0;
        this.CodigoPostal = 0;
        this.IdLocalidad = 0;
        this.Domicilio = 0;
        this.CorreoElectronico = "";
        this.Telefono = 0;
    }
    static async GuardarEntregaBeneficio(IdAfiliado, TempObjDomicilio, CorreoElectronico, Telefono) {
        try {
            let ObjGuardar = new EntregaBeneficio;
            ObjGuardar.IdTitular = IdAfiliado;
            ObjGuardar.CodigoPostal = TempObjDomicilio.CodigoPostal;
            ObjGuardar.IdLocalidad = TempObjDomicilio.IdLocalidad;
            ObjGuardar.Domicilio = TempObjDomicilio.Domicilio;
            ObjGuardar.CorreoElectronico = CorreoElectronico;
            ObjGuardar.Telefono = Telefono;
            let data = ObjGuardar;
            let entidad = "EntregaBeneficios";
            let metodo = "Alta";
            let url = ApiURL + "/" + entidad + "/" + metodo;
            return await PostAPI(url, data);
        } catch (error) {
            throw new Error(error);
        }
    }
}