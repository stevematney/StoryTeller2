using System.Collections.Generic;
using System.Linq;
using FubuCore.CommandLine;
using HtmlTags;
using ST.Docs.Topics;
using ST.Docs.Transformation;

namespace ST.Docs.Commands
{
    public class CommandUsageBodyTransformHandler : ITransformHandler
    {
        private readonly ICommandUsageCache _cache;

        public CommandUsageBodyTransformHandler(ICommandUsageCache cache)
        {
            _cache = cache;
        }

        public string Key
        {
            get { return "/command-usage"; }
        }

        public string Transform(Topic current, string data)
        {
            var parts = data.Split('/');
            var app = parts.First();
            var command = parts.Last();

            var report = _cache.ReportFor(app, command);

            return CommandBodyTags(app, report).ToTagList().ToString();
        }

        public static IEnumerable<HtmlTag> CommandBodyTags(string applicationName, CommandReport report)
        {
            if (report.Usages.Count() == 1)
            {
                yield return new SingleUsageTag(report.Usages.Single());
            }
            else
            {
                yield return new UsageTableTag(report);
            }

            if (report.Arguments.Any())
            {
                yield return new ArgumentsTag(report);
            }

            if (report.Flags.Any())
            {
                yield return new FlagsTag(report);
            }
        }
    }
}