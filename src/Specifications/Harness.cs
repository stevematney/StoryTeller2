﻿using FubuCore;
using StoryTeller;
using StoryTeller.Results;

namespace Specifications
{
    public class Harness
    {
        public void RunTransformations()
        {
            var runner = new SpecRunner<SpecificationSystem>();

            var results = runner.Run("Docs/Outline Generation");
            //runner.RunAll(1.Minutes());

            var document = runner.GenerateResultsDocument();

            document.OpenInBrowser();

            //results.Counts.AssertSuccess();
        } 
    }
}