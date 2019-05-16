﻿using System;

namespace Doctrina.Domain.Entities.Documents
{
    public interface IActivityProfileEntity
    {
        ActivityEntity Activity { get; set; }

        string ActivityHash { get; set; }

        DocumentEntity Document { get; set; }

        Guid Key { get; set; }

        string ProfileId { get; set; }

        Guid? RegistrationId { get; set; }

        DateTime UpdateDate { get; set; }
    }
}