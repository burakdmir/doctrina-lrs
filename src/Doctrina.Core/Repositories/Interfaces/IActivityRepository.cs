﻿using System;
using Doctrina.Core.Data;

namespace Doctrina.Core.Repositories
{
    public interface IActivityRepository
    {
        ActivityEntity GetByActivityId(string activityId);
        ActivityEntity GetByActivityId(Uri activityId);
        ActivityEntity Create(ActivityEntity entity);
        void Update(ActivityEntity entity);
    }
}