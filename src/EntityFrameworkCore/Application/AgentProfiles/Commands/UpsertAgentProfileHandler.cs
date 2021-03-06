using AutoMapper;
using Doctrina.Application.AgentProfiles.Commands;
using Doctrina.Application.AgentProfiles.Queries;
using Doctrina.Application.Agents.Commands;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Documents;
using Doctrina.ExperienceApi.Data.Documents;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Doctrina.Persistence.Infrastructure;

namespace Doctrina.Application.AgentProfiles
{
    public class UpsertAgentProfileHandler : IRequestHandler<UpsertAgentProfileCommand, AgentProfileEntity>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UpsertAgentProfileHandler(IDoctrinaDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<AgentProfileEntity> Handle(UpsertAgentProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = await _mediator.Send(GetAgentProfileQuery.Create(request.Agent, request.ProfileId), cancellationToken);
            if (profile == null)
            {
                return await _mediator.Send(
                    CreateAgentProfileCommand.Create(request.Agent, request.ProfileId, request.Content, request.ContentType),
                cancellationToken);
            }

            return await _mediator.Send(UpdateAgentProfileCommand.Create(request.Agent, request.ProfileId, request.Content, request.ContentType), cancellationToken);
        }

    }
}
