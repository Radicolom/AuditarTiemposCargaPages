#region Usings
using Dominio.Entities;
using Dominio.ModuloPages.Repositorio;
using System;
#endregion

public class PagesServices
{
    #region Atributos
    private readonly IAuditarPaginaRepository _auditarPaginaRepository;
    private readonly IAuditarLogRepository _auditarLogRepository;
    #endregion

    #region constructores
    public PagesServices(
        IAuditarPaginaRepository auditarPaginaRepository,
        IAuditarLogRepository auditarLogRepository)
    {
        _auditarPaginaRepository = auditarPaginaRepository;
        _auditarLogRepository = auditarLogRepository;
    }
    #endregion

    #region AuditarPagina
    private List<AuditarPaginaVista> MapToAuditarPaginaVista(List<AuditarPagina> paginas)
    {
        return paginas.Select(p => new AuditarPaginaVista
        {
            Id = p.AuditarPaginaId,
            Url = p.UrlAuditarPagina,
            Nombre = p.NombreAuditarPagina,
            Estado = p.EstadoAuditarPagina,
            FechaCreacion = p.FechaCreacionAuditarPagina,
            UsuarioId = p.UsuarioId,
            UsuarioNombre = p.Usuario?.NombreUsuario
        }).ToList();
    }

    private AuditarPagina MapToAuditarPagina(AuditarPaginaVista vista)
    {
        return new AuditarPagina
        {
            AuditarPaginaId = vista.Id ?? 0,
            UrlAuditarPagina = vista.Url ?? string.Empty,
            NombreAuditarPagina = vista.Nombre ?? string.Empty,
            EstadoAuditarPagina = vista.Estado,
            FechaCreacionAuditarPagina = vista.FechaCreacion,
            UsuarioId = vista.UsuarioId
        };
    }

    public RespuestaApp<AuditarPaginaVista> AuditarPaginaObtener(int? id = null)
    {
        var respuesta = new RespuestaApp<AuditarPaginaVista>();
        try
        {
            var paginas = _auditarPaginaRepository.GetByAuditarPaginaAsync(id);
            respuesta.Vista = MapToAuditarPaginaVista(paginas);
            respuesta.OperacionExitosa = true;
            respuesta.ValidacionesNegocio = false;
        }
        catch (Exception ex)
        {
            respuesta.OperacionExitosa = false;
            respuesta.ValidacionesNegocio = false;
            respuesta.Mensaje = ex.Message;
        }
        return respuesta;
    }

    public async Task<RespuestaApp<AuditarPaginaVista>> AddAuditarPaginaAsync(AuditarPaginaVista vista)
    {
        var respuesta = new RespuestaApp<AuditarPaginaVista>();
        try
        {
            var entity = MapToAuditarPagina(vista);
            var result = await _auditarPaginaRepository.AddAuditarPaginaAsync(entity);
            if (result != null)
            {
                respuesta.Vista.Add(new AuditarPaginaVista
                {
                    Id = result.AuditarPaginaId,
                    Url = result.UrlAuditarPagina,
                    Nombre = result.NombreAuditarPagina,
                    Estado = result.EstadoAuditarPagina,
                    FechaCreacion = result.FechaCreacionAuditarPagina,
                    UsuarioId = result.UsuarioId,
                    UsuarioNombre = result.Usuario?.NombreUsuario
                });
                respuesta.OperacionExitosa = true;
                respuesta.ValidacionesNegocio = false;
            }
            else
            {
                respuesta.OperacionExitosa = false;
                respuesta.ValidacionesNegocio = false;
            }
        }
        catch (Exception ex)
        {
            respuesta.OperacionExitosa = false;
            respuesta.ValidacionesNegocio = false;
            respuesta.Mensaje = ex.Message;
        }
        return respuesta;
    }

    public async Task<RespuestaApp<AuditarPaginaVista>> UpdateAuditarPaginaAsync(AuditarPaginaVista vista)
    {
        var respuesta = new RespuestaApp<AuditarPaginaVista>();
        try
        {
            var entity = MapToAuditarPagina(vista);
            var result = await _auditarPaginaRepository.UpdateAuditarPaginaAsync(entity);
            if (result != null)
            {
                respuesta.Vista.Add(new AuditarPaginaVista
                {
                    Id = result.AuditarPaginaId,
                    Url = result.UrlAuditarPagina,
                    Nombre = result.NombreAuditarPagina,
                    Estado = result.EstadoAuditarPagina,
                    FechaCreacion = result.FechaCreacionAuditarPagina,
                    UsuarioId = result.UsuarioId,
                    UsuarioNombre = result.Usuario?.NombreUsuario
                });
                respuesta.OperacionExitosa = true;
                respuesta.ValidacionesNegocio = false;
            }
            else
            {
                respuesta.OperacionExitosa = false;
                respuesta.ValidacionesNegocio = false;
            }
        }
        catch (Exception ex)
        {
            respuesta.OperacionExitosa = false;
            respuesta.ValidacionesNegocio = false;
            respuesta.Mensaje = ex.Message;
        }
        return respuesta;
    }

    public async Task<RespuestaApp<bool>> DeleteAuditarPaginaAsync(int id)
    {
        var respuesta = new RespuestaApp<bool>();
        try
        {
            var eliminado = await _auditarPaginaRepository.DeleteAuditarPaginaAsync(id);
            respuesta.Vista.Add(eliminado);
            respuesta.OperacionExitosa = eliminado;
            respuesta.ValidacionesNegocio = eliminado;
        }
        catch (Exception ex)
        {
            respuesta.OperacionExitosa = false;
            respuesta.ValidacionesNegocio = false;
            respuesta.Mensaje = ex.Message;
        }
        return respuesta;
    }
    #endregion

    #region AuditarLog
    private List<AuditarLogVista> MapToAuditarLogVista(List<AuditarLog> logs)
    {
        return logs.Select(log => new AuditarLogVista
        {
            Id = log.AuditarLogId,
            AuditarPaginaId = log.AuditarPaginaId,
            FechaCreacion = log.FechaCreacion,
            EstadoAuditarPagina = log.EstadoAuditarPagina,
            PerformanceScore = log.PerformanceScore,
            TimeToFirstByteMs = log.TimeToFirstByteMs,
            DomProcessingTimeMs = log.DomProcessingTimeMs,
            PageLoadTimeMs = log.PageLoadTimeMs,
            FcpValue = log.FcpValue,
            LcpValue = log.LcpValue,
            ClsValue = log.ClsValue,
            SpeedIndexValue = log.SpeedIndexValue,
			Nombre = log.AuditarPagina?.NombreAuditarPagina,
            Url = log.AuditarPagina?.UrlAuditarPagina,
			FechaCreacionAuditarPagina = log.AuditarPagina?.FechaCreacionAuditarPagina,
			UsuarioCreacion = log.AuditarPagina?.Usuario?.NombreUsuario + ' ' + log.AuditarPagina?.Usuario?.ApellidoUsuario

		}).ToList();
    }

    private AuditarLog MapToAuditarLog(AuditarLogVista vista)
    {
        return new AuditarLog
        {
            AuditarLogId = vista.Id,
            AuditarPaginaId = vista.AuditarPaginaId,
            FechaCreacion = vista.FechaCreacion,
            EstadoAuditarPagina = vista.EstadoAuditarPagina,
            PerformanceScore = vista.PerformanceScore,
            TimeToFirstByteMs = vista.TimeToFirstByteMs,
            DomProcessingTimeMs = vista.DomProcessingTimeMs,
            PageLoadTimeMs = vista.PageLoadTimeMs,
            FcpValue = vista.FcpValue,
            LcpValue = vista.LcpValue,
            ClsValue = vista.ClsValue,
            SpeedIndexValue = vista.SpeedIndexValue
		};
    }

    public RespuestaApp<AuditarLogVista> AuditarLogObtener(AuditarLogVista auditarLogVista)
    {
        var respuesta = new RespuestaApp<AuditarLogVista>();
        try
        {
            var dataFilter = new AuditarLog
            {
                AuditarLogId = auditarLogVista.Id,
                AuditarPaginaId = auditarLogVista.AuditarPaginaId,
                FechaCreacion = auditarLogVista.FechaCreacion,
                EstadoAuditarPagina = auditarLogVista.EstadoAuditarPagina,
                PerformanceScore = auditarLogVista.PerformanceScore,
                TimeToFirstByteMs = auditarLogVista.TimeToFirstByteMs,
                DomProcessingTimeMs = auditarLogVista.DomProcessingTimeMs,
                PageLoadTimeMs = auditarLogVista.PageLoadTimeMs,
                FcpValue = auditarLogVista.FcpValue,
                LcpValue = auditarLogVista.LcpValue,
                ClsValue = auditarLogVista.ClsValue,
                SpeedIndexValue = auditarLogVista.SpeedIndexValue
            };


			var logs = _auditarLogRepository.GetByAuditarLogAsync(dataFilter);
            respuesta.Vista = MapToAuditarLogVista(logs);
            respuesta.OperacionExitosa = true;
            respuesta.ValidacionesNegocio = false;
        }
        catch (Exception ex)
        {
            respuesta.OperacionExitosa = false;
            respuesta.ValidacionesNegocio = false;
            respuesta.Mensaje = ex.Message;
        }
        return respuesta;
    }

    public async Task<RespuestaApp<AuditarLogVista>> AddAuditarLogAsync(AuditarLogVista vista)
    {
        var respuesta = new RespuestaApp<AuditarLogVista>();
        try
        {
            var entity = MapToAuditarLog(vista);
            var result = await _auditarLogRepository.AddAuditarLogAsync(entity);
            if (result != null)
            {
                respuesta.Vista.Add(new AuditarLogVista
                {
                    Id = result.AuditarLogId,
                    AuditarPaginaId = result.AuditarPaginaId,
                    FechaCreacion = result.FechaCreacion,
                    EstadoAuditarPagina = result.EstadoAuditarPagina,
                    PerformanceScore = result.PerformanceScore,
                    TimeToFirstByteMs = result.TimeToFirstByteMs,
                    DomProcessingTimeMs = result.DomProcessingTimeMs,
                    PageLoadTimeMs = result.PageLoadTimeMs,
                    FcpValue = result.FcpValue,
                    LcpValue = result.LcpValue,
                    ClsValue = result.ClsValue,
                    SpeedIndexValue = result.SpeedIndexValue
                });
                respuesta.OperacionExitosa = true;
                respuesta.ValidacionesNegocio = false;
            }
            else
            {
                respuesta.OperacionExitosa = false;
                respuesta.ValidacionesNegocio = false;
            }
        }
        catch (Exception ex)
        {
            respuesta.OperacionExitosa = false;
            respuesta.ValidacionesNegocio = false;
            respuesta.Mensaje = ex.Message;
        }
        return respuesta;
    }

    public async Task<RespuestaApp<AuditarLogVista>> UpdateAuditarLogAsync(AuditarLogVista vista)
    {
        var respuesta = new RespuestaApp<AuditarLogVista>();
        try
        {
            var entity = MapToAuditarLog(vista);
            var result = await _auditarLogRepository.UpdateAuditarLogAsync(entity);
            if (result != null)
            {
                respuesta.Vista.Add(new AuditarLogVista
                {
                    Id = result.AuditarLogId,
                    AuditarPaginaId = result.AuditarPaginaId,
                    FechaCreacion = result.FechaCreacion,
                    EstadoAuditarPagina = result.EstadoAuditarPagina,
                    PerformanceScore = result.PerformanceScore,
                    TimeToFirstByteMs = result.TimeToFirstByteMs,
                    DomProcessingTimeMs = result.DomProcessingTimeMs,
                    PageLoadTimeMs = result.PageLoadTimeMs,
                    FcpValue = result.FcpValue,
                    LcpValue = result.LcpValue,
                    ClsValue = result.ClsValue,
                    SpeedIndexValue = result.SpeedIndexValue
                });
                respuesta.OperacionExitosa = true;
                respuesta.ValidacionesNegocio = false;
            }
            else
            {
                respuesta.OperacionExitosa = false;
                respuesta.ValidacionesNegocio = false;
            }
        }
        catch (Exception ex)
        {
            respuesta.OperacionExitosa = false;
            respuesta.ValidacionesNegocio = false;
            respuesta.Mensaje = ex.Message;
        }
        return respuesta;
    }

    public async Task<RespuestaApp<bool>> DeleteAuditarLogAsync(int id)
    {
        var respuesta = new RespuestaApp<bool>();
        try
        {
            var eliminado = await _auditarLogRepository.DeleteAuditarLogAsync(id);
            respuesta.Vista.Add(eliminado);
            respuesta.OperacionExitosa = eliminado;
            respuesta.ValidacionesNegocio = eliminado;
        }
        catch (Exception ex)
        {
            respuesta.OperacionExitosa = false;
            respuesta.ValidacionesNegocio = false;
            respuesta.Mensaje = ex.Message;
        }
        return respuesta;
    }
    #endregion
}
