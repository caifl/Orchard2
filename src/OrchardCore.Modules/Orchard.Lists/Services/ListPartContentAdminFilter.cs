﻿using System.Linq;
using System.Threading.Tasks;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.Records;
using Orchard.Contents.Services;
using Orchard.Contents.ViewModels;
using Orchard.DisplayManagement.ModelBinding;
using Orchard.Lists.Indexes;
using Orchard.Lists.Models;
using Orchard.Navigation;
using YesSql;
using YesSql.Services;

namespace Orchard.Lists.Services
{
    public class ListPartContentAdminFilter : IContentAdminFilter
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public ListPartContentAdminFilter(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public async Task FilterAsync(IQuery<ContentItem> query, ListContentsViewModel model, PagerParameters pagerParameters, IUpdateModel updateModel)
        {
            var viewModel = new ListPartContentAdminFilterModel();
            if(await updateModel.TryUpdateModelAsync(viewModel, ""))
            {
                // Show list content items 
                if (viewModel.ShowListContentTypes)
                {
                    var listableTypes = _contentDefinitionManager
                        .ListTypeDefinitions()
                        .Where(x =>
                            x.Parts.Any(p =>
                                p.PartDefinition.Name == nameof(ListPart)))
                        .Select(x => x.Name);

                    query.With<ContentItemIndex>(x => x.ContentType.IsIn(listableTypes));
                }

                // Show contained elements for the specified list
                else if(viewModel.ListContentItemId != null)
                {
                    query.With<ContainedPartIndex>(x => x.ListContentItemId == viewModel.ListContentItemId);
                }
            }
        }
    }

    public class ListPartContentAdminFilterModel
    {
        public bool ShowListContentTypes { get; set; }
        public string ListContentItemId { get; set; }
    }
}
