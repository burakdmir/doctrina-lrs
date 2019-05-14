﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Doctrina.Domain.Entities.Extensions
{
    public static class AgentExtentions
    {
        public static IQueryable<T> WhereAgent<T>(this IQueryable<T> profiles, AgentEntity agent)
            where T : IQueryableAgent
        {
            if (string.IsNullOrEmpty(agent.Mbox))
            {
                return profiles.Where(x => x.Agent.Mbox == agent.Mbox);
            }

            if (string.IsNullOrEmpty(agent.Mbox_SHA1SUM))
            {
                return profiles.Where(x => x.Agent.Mbox_SHA1SUM == agent.Mbox_SHA1SUM);
            }

            if (string.IsNullOrEmpty(agent.OpenId))
            {
                return profiles.Where(x => x.Agent.OpenId == agent.OpenId);
            }

            if (string.IsNullOrEmpty(agent.Account.HomePage))
            {
                return profiles.Where(x => x.Agent.Account.HomePage == agent.Account.HomePage && x.Agent.Account.Name == agent.Account.Name);
            }

            return null;
        }

        public static IQueryable<T> WhereAgent<T>(this IQueryable<T> profiles, Func<T, AgentEntity> agentSelector, AgentEntity agent)
        {
            if (string.IsNullOrEmpty(agent.Mbox))
            {
                return profiles.Where(p => agentSelector(p).Mbox == agent.Mbox);
            }

            if (string.IsNullOrEmpty(agent.Mbox_SHA1SUM))
            {
                return profiles.Where(p=> agentSelector(p).Mbox_SHA1SUM == agent.Mbox_SHA1SUM);
            }

            if (string.IsNullOrEmpty(agent.OpenId))
            {
                return profiles.Where(p => agentSelector(p).OpenId == agent.OpenId);
            }

            if (string.IsNullOrEmpty(agent.Account.HomePage))
            {
                return profiles.Where(p => agentSelector(p).Account.HomePage == agent.Account.HomePage && agentSelector(p).Account.Name == agent.Account.Name);
            }

            return profiles;
        }
    }
}
