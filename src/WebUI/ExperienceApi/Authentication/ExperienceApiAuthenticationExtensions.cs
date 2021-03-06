using Microsoft.AspNetCore.Authentication;
using System;

namespace Doctrina.WebUI.ExperienceApi.Authentication
{
    public static class ExperienceApiAuthenticationExtensions
    {
        public static AuthenticationBuilder AddExperienceApiAuthentication(this AuthenticationBuilder builder, Action<ExperienceApiAuthenticationOptions> configureOptions)
        {
            return builder.AddScheme<ExperienceApiAuthenticationOptions, ExperienceApiAuthenticationHandler>(ExperienceApiAuthenticationOptions.DefaultScheme, "Experience API Authority", configureOptions);
        }
    }
}