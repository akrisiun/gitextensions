        public Encoding FilesContentEncoding { get; private set; }
        /// <returns></returns>
            bool combinedDiff;
            if (!IsStartOfANewPatch(input, out combinedDiff))
                //header lines are encoded in GitModule.SystemEncoding

                    //TODO: NOT SUPPORTED!
                    patch.Apply = false;

                    //TODO: NOT SUPPORTED!
                    patch.Apply = false;
                    //diff content
                    //warnings, messages ...
        /// <param name="patchText"></param>
        /// <returns></returns>
        private void ValidateHeader(ref string input, Patch patch)
            //means there is no old file, so this should be a new file
            //line starts with --- means, old file name
            //line starts with +++ means, new file name
            //we expect a new file now!
            bool combinedDiff;
            return IsStartOfANewPatch(input, out combinedDiff);