using System;
using FubuCore;
using FubuMVC.Core.Runtime.Files;

namespace ST.Docs.Samples
{
    public class SnippetReader
    {
        private readonly IFubuFile _file;
        private readonly ISnippetScanner _scanner;
        private readonly ISnippetCache _cache;
        private Action<string, int> _readAction;

        public SnippetReader(IFubuFile file, ISnippetScanner scanner, ISnippetCache cache)
        {
            _file = file;
            _scanner = scanner;
            _cache = cache;
        }

        public void Start()
        {
            _readAction = lookForNewSnippet;

            var line = 0;
            _file.ReadContents().ReadLines(text =>
            {
                line++;
                _readAction(text, line);
            });
        }

        private void lookForNewSnippet(string text, int lineNumber)
        {
            var name = _scanner.DetermineName(text);

            if (name.IsNotEmpty())
            {
                var snippet = new Snippet(name){
                    Language = _scanner.Language,
                    File = _file.RelativePath
                };

                _readAction = (txt, num) =>
                {
                    if (_scanner.IsAtEnd(txt))
                    {
                        _cache.AddOrReplace(snippet);
                        _readAction = lookForNewSnippet;
                    }
                    else
                    {
                        snippet.Append(txt, num);
                    }
                };
            }
        }
    }
}