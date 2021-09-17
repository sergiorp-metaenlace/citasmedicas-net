using AutoMapper;
using CitaMedica.Models.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CitaMedica.Models.MapperProfiles
{
    public class CitaMedicaProfile : Profile
    {
        public CitaMedicaProfile()
        {
            CreateMap<Usuario, UsuarioDTO>();

            CreateMap<Cita, CitaDTO>()
                .ForMember(cdto => cdto.FechaHora, o => o.MapFrom(cita => cita.FechaHora.ToString("dd-MM-yyyy HH:mm")))
                .ForMember(cdto => cdto.Medico, o => o.MapFrom(cita => cita.Medico.Id))
                .ForMember(cdto => cdto.Paciente, o => o.MapFrom(cita => cita.Paciente.Id))
                .ForMember(cdto => cdto.Diagnostico, o => o.MapFrom(cita => cita.Diagnostico.Id));

            CreateMap<Diagnostico, DiagnosticoDTO>();

            CreateMap<Medico, MedicoDTO>()
                .ForMember(mdto => mdto.Pacientes, o => o.MapFrom(med => med.Pacientes.Select(p => p.Id).ToList()))
                .ForMember(mdto => mdto.User, o => o.MapFrom(med => med.User))
                .ForMember(mdto => mdto.Id, o => o.MapFrom(med => med.Id))
                .ForMember(mdto => mdto.Nombre, o => o.MapFrom(med => med.Nombre))
                .ForMember(mdto => mdto.Apellidos, o => o.MapFrom(med => med.Apellidos));

            CreateMap<Paciente, PacienteDTO>()
                .ForMember(pdto => pdto.Medicos, o => o.MapFrom(pac => pac.Medicos.Select(m => m.Id).ToList()))
                .ForMember(pdto => pdto.User, o => o.MapFrom(pac => pac.User))
                .ForMember(pdto => pdto.Id, o => o.MapFrom(pac => pac.Id))
                .ForMember(pdto => pdto.Nombre, o => o.MapFrom(pac => pac.Nombre))
                .ForMember(pdto => pdto.Apellidos, o => o.MapFrom(pac => pac.Apellidos));

            CreateMap<PacienteDTO, Paciente>()
                .ForMember(pac => pac.Medicos, o => o.MapFrom(dto => new List<Medico>()))
                .ForMember(pac => pac.User, o => o.MapFrom(dto => dto.User));

            CreateMap<MedicoDTO, Medico>()
                .ForMember(med => med.Pacientes, o => o.MapFrom(dto => new List<Paciente>()))
                .ForMember(med => med.User, o => o.MapFrom(dto => dto.User));

            CreateMap<CitaDTO, Cita>()
                .ForMember(cita => cita.FechaHora, o => o.MapFrom(dto => DateTime.ParseExact(dto.FechaHora, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture)))
                .ForMember(cita => cita.Medico, o => o.Ignore())
                .ForMember(cita => cita.Paciente, o => o.Ignore())
                .ForMember(cita => cita.Diagnostico, o => o.Ignore());
        }
    }
}
