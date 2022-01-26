using hhnl.HomeAssistantNet.Automations.Automation;
using hhnl.HomeAssistantNet.Shared.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace hhnl.HomeAssistantNet.Automations.BuildingBlocks
{
    public static class Secrets
    {
        /// <summary>
        /// Checks if a secret with the given key exists.
        /// </summary>
        /// <param name="key">The key of the secret.</param>
        /// <returns><c>true</c> when the key was found; otherwise <c>false</c>.</returns>
        public static bool HasSecret(string key)
        {
            return GetSecrets().ContainsKey(key);
        }

        /// <summary>
        /// Gets a secret with the given key.
        /// </summary>
        /// <param name="key">The key of the secret.</param>
        /// <returns>The value of the secret.</returns>
        /// <exception cref="ArgumentException">Thrown when no secret with the given key was found.</exception>
        public static string GetSecret(string key)
        {
            if (!HasSecret(key))
            {
                throw new ArgumentException($"No scecret with the key '{key}' found", nameof(key));
            }

            return GetSecrets()[key];
        }

        /// <summary>
        /// Gets all secrets.
        /// </summary>
        /// <returns>A dictionary of all secre keys and values.</returns>
        public static IReadOnlyDictionary<string, string> GetSecrets()
        {
            return AutomationRunContext.GetRunContextOrFail().ServiceProvider.GetRequiredService<IOptionsSnapshot<AutomationSecrets>>().Value;
        }
    }
}
