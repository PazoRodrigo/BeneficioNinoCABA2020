
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
        // if (datos?.length > 0) {
        //     let i = 0;
        //     while (i <= datos.length - 1) {
        //         result.push(llenarEntidadBeneficio(datos[i]));
        //         i++;
        //     }
        // }
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
                let Nombre = ObjFamiliar[i].ApellidoNombre;
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
    static async TraerTodos() {
        let entidad = 'EntregaBeneficios';
        let metodo = 'TraerTodos';
        let url = ApiURL + '/' + entidad + '/' + metodo;
        let datos = await TraerAPI(url);
        let result = [];
        $.each(datos, function (key, value) {
            result.push(llenarEntidadEntregaBeneficio(value));
        });
        return result;
    }


}
function llenarEntidadEntregaBeneficio(entidad) {
    let objResult = new Beneficio;
    objResult.IdEntidad = entidad.IdEntidad;
    objResult.IdTitular = entidad.IdTitular;
    objResult.CodigoPostal = entidad.CodigoPostal;
    objResult.IdLocalidad = entidad.IdLocalidad;
    objResult.Domicilio = entidad.Domicilio;
    objResult.CorreoElectronico = entidad.CorreoElectronico;
    objResult.Telefono = entidad.Telefono;
    return objResult;
}
class EntregaBeneficio_ReporteEntregas {
    constructor() {
        this.IdTitular = 0;
        this.Tit_Apellido = '';
        this.Tit_Nombre = '';
        this.Tit_NroDocumento = 0;
        this.Tit_Ape_Sind = '';
        this.Tit_NroSindical = 0;
        this.Telefono = 0;
        this.Fam_Apellido = '';
        this.Fam_Nombre = '';
        this.TipoBeneficio = '';
        this.Domicilio = '';
        this.CodigoPostal = 0;
        this.Localidad = '';
        this.RazonSocial = '';
        this.FechaSolicitudEntrega = 0;
        this.CorreoElectronico = '';
    }
    static async TraerTodosReporte_Entregas() {
        let entidad = 'EntregaBeneficios';
        let metodo = 'TraerTodosReporte_Entregas';
        let url = ApiURL + '/' + entidad + '/' + metodo;
        let datos = await TraerAPI(url);
        let result = [];
        $.each(datos, function (key, value) {
            result.push(llenarEntidadReporte_Entrega(value));
        });
        return result;
    }
    static async TraerTodosReporte_Solicitados() {
        let entidad = 'EntregaBeneficios';
        let metodo = 'TraerTodosReporte_Solicitados';
        let url = ApiURL + '/' + entidad + '/' + metodo;
        let datos = await TraerAPI(url);
        let result = [];
        $.each(datos, function (key, value) {
            result.push(llenarEntidadReporte_Solicitados(value));
        });
        return result;
    }

    static async ArmarGrillaImpresionReporte(div, Lista) {
        $('#' + div + '').html('');
        let str = '';
        if (Lista.length > 0) {
            str += ' <table class="table table-bordered">';
            str += '     <thead>';
            str += '         <tr class="bg-primary text-light">';
            str += '             <th class="text-center">Id Rep.</th>';
            str += '             <th class="text-center">Apellido Titular</th>';
            str += '             <th class="text-center">Nombre Titular</th>';
            str += '             <th class="text-center">Nro Documento</th>';
            str += '             <th class="text-center">Afil / Apor</th>';
            str += '             <th class="text-center">Nro. Sindical</th>';
            str += '             <th class="text-center">Telefono</th>';
            str += '             <th class="text-center">Apellido Familiar</th>';
            str += '             <th class="text-center">Nombre Familiar</th>';
            str += '             <th class="text-center">Beneficio</th>';
            str += '             <th class="text-center">Domicilio</th>';
            str += '             <th class="text-center">Código Postal</th>';
            str += '             <th class="text-center">Localidad</th>';
            str += '             <th class="text-center">Entidad</th>';
            str += '             <th class="text-center">Alta Solic. Entrega</th>';
            str += '         </tr>';
            str += '     </thead>';
            str += '     <tbody>';
            for (let item of Lista) {
                str += '    <tr>';
                str += '        <td class="text-right pr-1"> ' + item.IdTitular + '</td>';
                str += '             <td class="text-center">' + Left(item.Tit_Apellido, 30) + '</td>';
                str += '             <td class="text-center">' + Left(item.Tit_Nombre, 30) + '</td>';
                str += '             <td class="text-center">' + Right('00000000' + item.Tit_NroDocumento, 8) + '</td>';
                str += '             <td class="text-center">' + item.Tit_Ape_Sind + '</td>';
                str += '             <td class="text-center">' + item.Tit_NroSindical + '</td>';
                str += '             <td class="text-center">' + Right('00000000' + item.Telefono, 8) + '</td>';
                str += '             <td class="text-center">' + Left(item.Fam_Apellido, 30) + '</td>';
                str += '             <td class="text-center">' + Left(item.Fam_Nombre, 30) + '</td>';
                str += '             <td class="text-center">' + item.TipoBeneficio + '</td>';
                str += '             <td class="text-center">' + item.Domicilio + '</td>';
                str += '             <td class="text-center">' + item.CodigoPostal + '</td>';
                str += '             <td class="text-center">' + item.Localidad + '</td>';
                str += '             <td class="text-center">' + item.RazonSocial + '</td>';
                str += '             <td class="text-center">' + LongToDateString(item.FechaSolicitudEntrega) + '</td>';
                str += '    </tr>';
            }
            str += '     <t/body>';
            str += ' </table>';
        }
        return $('#' + div + '').html(str);
    }
}
function llenarEntidadReporte_Entrega(entidad) {
    let objResult = new EntregaBeneficio_ReporteEntregas;
    objResult.IdTitular = entidad.IdTitular;
    objResult.Tit_Apellido = entidad.Tit_Apellido;
    objResult.Tit_Nombre = entidad.Tit_Nombre;
    objResult.Tit_NroDocumento = entidad.Tit_NroDocumento;
    objResult.Tit_Ape_Sind = entidad.Tit_Ape_Sind;
    objResult.Tit_NroSindical = entidad.Tit_NroSindical;
    objResult.Telefono = entidad.Telefono;
    objResult.Fam_Apellido = entidad.Fam_Apellido;
    objResult.Fam_Nombre = entidad.Fam_Nombre;
    objResult.TipoBeneficio = entidad.TipoBeneficio;
    objResult.Domicilio = entidad.Domicilio;
    objResult.CodigoPostal = entidad.CodigoPostal;
    objResult.Localidad = entidad.Localidad;
    objResult.RazonSocial = entidad.RazonSocial;
    objResult.FechaSolicitudEntrega = entidad.FechaSolicitudEntrega;
    objResult.CorreoElectronico = entidad.CorreoElectronico;
    return objResult;
}

class EntregaBeneficio_ReporteSolicitados {
    constructor() {
        this.IdTitular = 0;
        this.Tit_Apellido = '';
        this.Tit_Nombre = '';
        this.Tit_NroDocumento = 0;
        this.Tit_NroSindical = 0;
        this.TipoBeneficio = '';
        this.RazonSocial = '';
        this.FechaEntrega = 0;
        this.NroRemito = 0;
    }
    static async TraerTodosReporte_Solicitados() {
        let entidad = 'EntregaBeneficios';
        let metodo = 'TraerTodosReporte_Solicitados';
        let url = ApiURL + '/' + entidad + '/' + metodo;
        let datos = await TraerAPI(url);
        let result = [];
        $.each(datos, function (key, value) {
            result.push(llenarEntidadReporte_Solicitados(value));
        });
        return result;
    }
    static async ArmarGrillaImpresionReporte(div, Lista) {
        $('#' + div + '').html('');
        let str = '';
        if (Lista.length > 0) {
            str += ' <table class="table table-bordered">';
            str += '     <thead>';
            str += '         <tr class="bg-primary text-light">';
            str += '             <th class="text-center">Id Rep.</th>';
            str += '             <th class="text-center">Apellido</th>';
            str += '             <th class="text-center">Nombre</th>';
            str += '             <th class="text-center">Nro Documento</th>';
            str += '             <th class="text-center">Nro. Sindical</th>';
            str += '             <th class="text-center">Beneficio</th>';
            str += '             <th class="text-center">Entidad</th>';
            str += '             <th class="text-center">Entrega</th>';
            str += '             <th class="text-center">Nro. Remito</th>';
            str += '         </tr>';
            str += '     </thead>';
            str += '     <tbody>';
            for (let item of Lista) {
                let NroSindical = '';
                if (item.Tit_NroSindical > 0) {
                    NroSindical = item.Tit_NroSindical;
                }
                let NroRemito = '';
                if (item.NroRemito > 0) {
                    NroRemito = item.NroRemito;
                }
                str += '    <tr>';
                str += '        <td class="text-right pr-1"> ' + item.IdTitular + '</td>';
                str += '             <td class="text-center">' + Left(item.Tit_Apellido, 30) + '</td>';
                str += '             <td class="text-center">' + Left(item.Tit_Nombre, 30) + '</td>';
                str += '             <td class="text-center">' + Right('00000000' + item.Tit_NroDocumento, 8) + '</td>';
                str += '             <td class="text-center">' + NroSindical + '</td>';
                str += '             <td class="text-center">' + item.TipoBeneficio + '</td>';
                str += '             <td class="text-center">' + item.RazonSocial + '</td>';
                str += '             <td class="text-center">' + LongToDateString(item.FechaEntrega) + '</td>';
                str += '             <td class="text-center">' + NroRemito + '</td>';
                str += '    </tr>';
            }
            str += '     <t/body>';
            str += ' </table>';
        }
        return $('#' + div + '').html(str);
    }
}
function llenarEntidadReporte_Solicitados(entidad) {
    let objResult = new EntregaBeneficio_ReporteSolicitados;
    objResult.IdTitular = entidad.IdTitular;
    objResult.Tit_Apellido = entidad.Tit_Apellido;
    objResult.Tit_Nombre = entidad.Tit_Nombre;
    objResult.Tit_NroDocumento = entidad.Tit_NroDocumento;
    objResult.Tit_NroSindical = entidad.Tit_NroSindical;
    objResult.TipoBeneficio = entidad.TipoBeneficio;
    objResult.RazonSocial = entidad.RazonSocial;
    objResult.FechaEntrega = entidad.FechaEntrega;
    objResult.NroRemito = entidad.NroRemito;
    return objResult;
}