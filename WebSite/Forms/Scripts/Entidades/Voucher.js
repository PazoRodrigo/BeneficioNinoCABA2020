class Voucher {
  constructor() {
    this.IdEntidad = 0;
    this.IdTitular = 0;
    this.IdFamiliar = "";
    this.Codigo = "";
    this.Confirmado = 0;
    this.Fecha = 0;

    this.Domicilio = '';
    this.CodigoPostal = 0;
    this.IdLocalidad = 0;
  }

  static async TraerUnoXId(idVoucher) {
    try {
      let data = {
        idVoucher: idVoucher,
      };
      let entidad = "Voucher";
      let metodo = "TraerUnoxId";
      let url = ApiURL + "/" + entidad + "/" + metodo;
      let datos = await TraerAPI(url, data);
      let result = [];
      $.each(datos, function (key, value) {
        result.push(llenarEntidadVoucher(value));
      });
      return result[0];
    } catch (error) {
      throw new Error(error)
    }

  }

  static async TraerTodosxAfiliado(idTitular) {
    try {
      let data = {
        idAfiliado: idTitular,
      };
      let entidad = "Voucher";
      let metodo = "TraerTodosxAfiliado";
      let url = ApiURL + "/" + entidad + "/" + metodo;
      let datos = await TraerAPI(url, data);
      let result = [];
      $.each(datos, function (key, value) {
        result.push(llenarEntidadVoucher(value));
      });
      return result;
    } catch (error) {
      throw new Error(error)
    }

  }
  static async Guardar(ListaBeneficios, ObjDomicilio) {
    try {
      spinner();
      let i = 0;
      while (i <= ListaBeneficios.length - 1) {
        let ObjVoucher = new Voucher;
        ObjVoucher.IdTitular = ListaBeneficios[i].IdTitular;
        ObjVoucher.IdFamiliar = ListaBeneficios[i].IdFamiliar;
        ObjVoucher.Domicilio = ObjDomicilio.Domicilio;
        ObjVoucher.CodigoPostal = ObjDomicilio.CodigoPostal;
        ObjVoucher.IdLocalidad = ObjDomicilio.IdLocalidad;
        await Voucher.Alta(ObjVoucher, ObjDomicilio);
        i++;
      }
      spinnerClose();
    } catch (error) {
      spinnerClose();
      alertAlerta(error);
    }
  }
  static async Alta(entidadVoucher) {
    try {
      let data = entidadVoucher
      let entidad = 'Voucher';
      let metodo = 'Alta';
      let url = ApiURL + '/' + entidad + '/' + metodo;
      return await PostAPI(url, data);
    } catch (error) {
      throw new Error(error)
    }
  }

  static async ArmarGrilla(div, ListaBeneficios, ListaFamiliares) {
    $('#' + div + '').html('');
    let str = '';
    let ListaImpresion = [];
    let f = 0;
    let Impresion;
    while (f <= ListaFamiliares.length - 1) {
      let b = 0;
      while (b <= ListaBeneficios.length - 1) {
        if (ListaBeneficios[b].IdFamiliar == ListaFamiliares[f].NroAfiliado) {
          Impresion = [];
          Impresion.ApellidoNombre = ListaFamiliares[f].ApellidoNombre;
          Impresion.FechaNacimiento = ListaFamiliares[f].FechaNacimiento;
          Impresion.NroDocumento = ListaFamiliares[f].NroDocumento;
          Impresion.Edad = ListaFamiliares[f].Edad;
          Impresion.Codigo = ListaBeneficios[b].Codigo;
          ListaImpresion.push(Impresion);
          b++;
        }
        f++;
      }
    }
    if (ListaImpresion.length > 0) {
      str += ' <table class="table table-bordered">';
      str += '     <thead>';
      str += '         <tr class="bg-primary text-light">';
      str += '             <th class="text-center">Nombre</th>';
      str += '             <th class="text-center">Nacimiento</th>';
      str += '             <th class="text-center">Nro. Documento</th>';
      str += '             <th class="text-center">Edad</th>';
      str += '             <th class="text-center">Id Voucher</th>';
      str += '         </tr>';
      str += '     </thead>';
      str += '     <tbody>';
      for (let item of ListaImpresion) {
        str += '    <tr>';
        str += '        <td class="text-left pl-1"> ' + item.ApellidoNombre + '</td>';
        str += '        <td class="text-center"> ' + LongToDateString(item.FechaNacimiento) + '</td>';
        str += '        <td class="text-center"> ' + Right('00000000' + item.NroDocumento, 8) + '</td>';
        str += '        <td class="text-right pr-4"> ' + item.Edad + '</td>';
        str += '        <td class="text-right pr-4"><small> ' + item.Codigo + '</small></td>';
        str += '    </tr>';
      }
      str += '     <t/body>';
      str += ' </table>';
    }
    return $('#' + div + '').html(str);
  }
}
function llenarEntidadVoucher(entidad) {
  let objResult = new Voucher();
  objResult.IdEntidad = entidad.IdEntidad;
  objResult.IdTitular = entidad.IdTitular;
  objResult.IdFamiliar = entidad.IdFamiliar;
  objResult.Codigo = entidad.Codigo;
  objResult.Confirmado = entidad.Confirmado;
  objResult.Fecha = entidad.Fecha;
  return objResult;
}
