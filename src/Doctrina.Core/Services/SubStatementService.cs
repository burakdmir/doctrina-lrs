﻿using Doctrina.Core.Data;
using Doctrina.Core.Repositories;
using System;
using Doctrina.xAPI.Models;

namespace Doctrina.Core.Services
{
    public class SubStatementService : StatementBaseService<SubStatementEntity>, ISubStatementService
    {
        private readonly ISubStatementRepository subStatements;
        private readonly IVerbService _verbService;
        private readonly IAgentService _agentService;

        public SubStatementService(DoctrinaContext dbContext, ISubStatementRepository subStatementRepository, IStatementRepository statementRepository, IVerbService verbService, IAgentService agentService, IActivityService activityService)
            : base(dbContext, statementRepository, agentService, activityService)
        {
            this.subStatements = subStatementRepository;
            this._verbService = verbService;
            this._agentService = agentService;
        }

        public SubStatementEntity CreateSubStatement(SubStatement model)
        {
            var verb = this._verbService.MergeVerb(model.Verb);
            var actor = this._agentService.MergeAgent(model.Actor);

            var entity = new SubStatementEntity()
            {
                ActorKey = actor.Key,
                VerbKey = verb.Key,
            };
            entity.Actor = actor;
            entity.Verb = verb;

            MergeTarget(entity, model.Target);
            MergeContext(entity, model.Context);
            MergeResult(entity, model.Result);
            try
            {
                this.subStatements.Create(entity);
            }
            catch (Exception e)
            {
                //LogHelper.Error<SubStatementService>("SaveSubStatement: \r\n {0}", new Exception(model.ToJson()));
                throw e;
            }

            return entity;
        }

        public override void MergeTarget(IStatementEntityBase stmt, StatementTargetBase target)
        {
            if (target == null)
                return;

            ObjectType objectType = target.ObjectType;
            if (objectType == ObjectType.SubStatement)
            {
                throw new NotImplementedException("SubStatements are not allowed as objectType of an SubStatement.");
            }

            base.MergeTarget(stmt, target);
        }
    }
}