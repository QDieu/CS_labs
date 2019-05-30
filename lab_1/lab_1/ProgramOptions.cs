    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace lab_1 {
    public class Options {
        [Option('l', "length",
            Default = -1,
            Required = false,
            HelpText = "maximum length of a sequence of differing bytes")]
        public long Length { get; set; }

        [Option('a', "text",
            Default = false,
            Required = false,
            HelpText = "output text, instead of character codes, if they are printed, and '.' if not printed")]
        public bool Text { get; set; }

        [Option('y', "side_by_side",
            Default = false,
            Required = false,
            HelpText = "display the difference in format")]
        public bool Side_by_side { get; set; }

        [Option('q', "brief",
            Default = false,
            Required = false,
            HelpText = "display the difference in format")]
        public bool Brief { get; set; }
    }
}
