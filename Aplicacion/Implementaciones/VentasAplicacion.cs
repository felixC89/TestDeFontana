using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using AutoMapper;
using Dominio.Interfaces;

namespace Aplicacion.Implementaciones
{
    public class VentasAplicacion : IVentasAplicacion
    {
        private readonly IMapper _mapper;
        private readonly IVentasDominio _ventas;

        public VentasAplicacion(IMapper mapper, IVentasDominio ventas)
        {
            _mapper = mapper;
            _ventas = ventas;
        }

        public async Task<IEnumerable<LocalDto>> GetLocal()
        {
            var resultDomain = await _ventas.GetLocal();

            var resultAplicacion = _mapper.Map<IEnumerable<LocalDto>>(resultDomain);

            return resultAplicacion;
        }

        public async Task<IEnumerable<MarcaDto>> GetMarcas()
        {
            var resultDomain = await _ventas.GetMarcas();

            var resultAplicacion = _mapper.Map<IEnumerable<MarcaDto>>(resultDomain);

            return resultAplicacion;
        }

        public async Task<IEnumerable<ProductoDto>> GetProductos()
        {
            var resultDomain = await _ventas.GetProductos();

            var resultAplicacion = _mapper.Map<IEnumerable<ProductoDto>>(resultDomain);

            return resultAplicacion;
        }

        public async Task<IEnumerable<VentaDto>> GetVentas(int dias)
        {
            var fechaf = new DateTime(2024, 04, 17);  //Se define fecha del 17 de abril ajustandome a las fechas que existen en la tabla.
            var fechai = fechaf.AddDays(-dias);

            var resultDomain = await _ventas.GetVentas(fechai, fechaf);

            var resultAplicacion = _mapper.Map<IEnumerable<VentaDto>>(resultDomain);

            return resultAplicacion;
        }

        public async Task<IEnumerable<VentaDetalleDto>> GetVentasDetalle()
        {
            var resultDomain = await _ventas.GetVentasDetalle();

            var resultAplicacion = _mapper.Map<IEnumerable<VentaDetalleDto>>(resultDomain);

            return resultAplicacion;
        }
    }
}
