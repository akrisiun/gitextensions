using System;

namespace GitCommands
{
    public interface IEnvironmentAbstraction
    {
        /// <summary>Terminates this process and returns an exit code to the operating system.</summary>
        /// <param name="exitCode">
        /// The exit code to return to the operating system. Use 0 (zero) to indicate that
        /// the process completed successfully.
        /// </param>
        void Exit(int exitCode);

        /// <summary>Returns a string array containing the command-line arguments for the current process.</summary>
        /// <returns>
        /// An array of string where each element contains a command-line argument. 
        /// The first element is the executable file name, and the following zero or more elements
        /// contain the remaining command-line arguments.
        /// </returns>
        string[] GetCommandLineArgs();

        string GetEnvironmentVariable(string variable);

        string SetEnvironmentVariable(string variable, string value);

        string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target);

        /// <summary>Gets the path to the system special folder that is identified by the specified enumeration.</summary>
        /// <returns>The path to the specified system special folder, if that folder physically exists on your computer; otherwise, an empty string ("").A folder will not physically exist if the operating system did not create it, the existing folder was deleted, or the folder is a virtual directory, such as My Computer, which does not correspond to a physical path.</returns>
        /// <param name="folder">An enumerated constant that identifies a system special folder.</param>
        string GetFolderPath(Environment.SpecialFolder folder);
    }

    public sealed class EnvironmentAbstraction : IEnvironmentAbstraction
    {
        /// <summary>Terminates this process and returns an exit code to the operating system.</summary>
        /// <param name="exitCode">
        /// The exit code to return to the operating system. Use 0 (zero) to indicate that
        /// the process completed successfully.
        /// </param>
        public void Exit(int exitCode)
        {
            Environment.Exit(exitCode);
        }

        /// <summary>Returns a string array containing the command-line arguments for the current process.</summary>
        /// <returns>
        /// An array of string where each element contains a command-line argument. 
        /// The first element is the executable file name, and the following zero or more elements
        /// contain the remaining command-line arguments.
        /// </returns>
        public string[] GetCommandLineArgs()
        {
            return Environment.GetCommandLineArgs();
        }

        public string GetEnvironmentVariable(string variable)
        {
            if (string.IsNullOrWhiteSpace(variable))
                return null;
            return Environment.GetEnvironmentVariable(variable);
        }

        public string SetEnvironmentVariable(string variable, string value)
        {
            Environment.SetEnvironmentVariable(variable, value);
            return GetEnvironmentVariable(variable);
        }

        public string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target)
        {
            return Environment.GetEnvironmentVariable(variable, target);
        }

        /// <summary>Gets the path to the system special folder that is identified by the specified enumeration.</summary>
        /// <returns>The path to the specified system special folder, if that folder physically exists on your computer; otherwise, an empty string ("").A folder will not physically exist if the operating system did not create it, the existing folder was deleted, or the folder is a virtual directory, such as My Computer, which does not correspond to a physical path.</returns>
        /// <param name="folder">An enumerated constant that identifies a system special folder.</param>
        public string GetFolderPath(Environment.SpecialFolder folder)
        {
            return Environment.GetFolderPath(folder);
        }
    }
}