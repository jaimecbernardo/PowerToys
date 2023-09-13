// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Microsoft.Windows.ApplicationModel.Resources;
using Windows.Foundation;

namespace Hosts.Helpers
{
    internal sealed class LanguageOverrideResourceManager : Microsoft.Windows.ApplicationModel.Resources.IResourceManager
    {
        private IResourceManager _internalResourceManager;
        private string _language;

        public LanguageOverrideResourceManager(string filename, string language)
        {
            _internalResourceManager = new ResourceManager(filename);
            _language = language;
        }

        ResourceMap IResourceManager.MainResourceMap => _internalResourceManager.MainResourceMap;

        event TypedEventHandler<ResourceManager, ResourceNotFoundEventArgs> IResourceManager.ResourceNotFound
        {
            add
            {
                _internalResourceManager.ResourceNotFound += value;
            }

            remove
            {
                _internalResourceManager.ResourceNotFound -= value;
            }
        }

        ResourceContext IResourceManager.CreateResourceContext()
        {
            var overrideResourceContext = _internalResourceManager.CreateResourceContext();
            overrideResourceContext.QualifierValues["Language"] = _language;
            return overrideResourceContext;
        }
    }
}
