using Markdig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jetsons.Jetpack {
	public static class MarkdownFormatter {


		public static string Format(string markdown) {

			if (markdown == "") {
				return "";
			}

			// Markdig with all extensions (markdown to HTML)
			var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
			var html = Markdown.ToHtml(markdown, pipeline);

			return html;
		}

	}
}
