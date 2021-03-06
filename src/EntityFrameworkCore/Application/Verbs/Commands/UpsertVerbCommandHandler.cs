﻿using AutoMapper;
using Doctrina.Application.Common.Interfaces;
using Doctrina.Application.Infrastructure;
using Doctrina.Domain.Entities;
using Doctrina.Domain.Entities.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using Doctrina.Persistence.Infrastructure;
using System.Threading.Tasks;
using Doctrina.Application.Verbs.Commands;

namespace Doctrina.Application.Verbs.Commands
{
    public class UpsertVerbCommandHandler : IRequestHandler<UpsertVerbCommand, VerbEntity>
    {
        private readonly IDoctrinaDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UpsertVerbCommandHandler(IDoctrinaDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<VerbEntity> Handle(UpsertVerbCommand request, CancellationToken cancellationToken)
        {
            string hash = request.Verb.Id.ComputeHash();
            VerbEntity verb = await _context.Verbs.SingleOrDefaultAsync(x => x.Hash == hash, cancellationToken);

            if(verb == null)
            {
                verb = _mapper.Map<VerbEntity>(request.Verb);
                verb.VerbId = Guid.NewGuid();
                _context.Verbs.Add(verb);
                return verb;
            }

            if (request.Verb.Display != null)
            {
                foreach (var dis in request.Verb.Display)
                {
                    if (verb.Display.ContainsKey(dis.Key))
                    {
                        verb.Display[dis.Key] = dis.Value;
                    }
                    else
                    {
                        verb.Display.Add(dis);
                    }
                }
                _context.Verbs.Update(verb);
            }

            return verb;
        }
    }
}
