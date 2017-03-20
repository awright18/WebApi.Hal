﻿using HalSample.Models;
using WebApi.Hal.MediaTypeFormatter;

namespace HalSample.HalConfigurations
{
    public class AuthorHalConfiguration : HalTypeConfiguration<Author>
    {
        public AuthorHalConfiguration() : base("/api/author")
        {
        }

        protected override void AddHalLinks(Author value)
        {
            if (value == null)
                return;

            AddLink("self", value.Id.ToString());
            AddLinkTemplate("find", "{?id}");
        }
    }
}