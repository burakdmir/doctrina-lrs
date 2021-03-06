﻿using AutoMapper;
using Doctrina.Application.Activities.Queries;
using Doctrina.Application.ActivityProfiles.Commands;
using Doctrina.Application.Common.Exceptions;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Domain.Entities.Documents;
using MediatR;
using Doctrina.Persistence.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.ActivityProfiles.Commands
{
    public class UpdateActivityProfileHandler : IRequestHandler<UpdateActivityProfileCommand>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UpdateActivityProfileHandler(IDoctrinaDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateActivityProfileCommand request, CancellationToken cancellationToken)
        {
            var activity = await _mediator.Send(GetActivityQuery.Create(request.ActivityId));

            if(activity == null)
            {
                throw new NotFoundException("No activity profiles for activity.");
            }

            ActivityProfileEntity profile = await _context.ActivityProfiles.GetProfileAsync(activity.ActivityId, request.ProfileId, request.Registration, cancellationToken);

            profile.Document.UpdateDocument(request.Content, request.ContentType);

            _context.ActivityProfiles.Update(profile);
            await _context.SaveChangesAsync(cancellationToken);

            return await Unit.Task;
        }
    }
}
