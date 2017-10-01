using System;
using System.Activities;
using System.Collections.Generic;
using System.Threading;

using Manifest.Contracts;

namespace GenerateManifest
{
    /// <summary>
    /// Invoke the workflow to generate the manifest
    /// </summary>
    public class InvokeManifestWorkflow
    {
        private static AutoResetEvent waitHandle = new AutoResetEvent(false);
        private Dictionary<string, string> arguments = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        private Exception wfException = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvokeManifestWorkflow"/> class.
        /// </summary>
        /// <param name="args">The args.</param>
        public InvokeManifestWorkflow(string[] args)
        {
            foreach (string s in args)
            {
                char[] possibleDelimiter = new char[] { ':', '=' };

                foreach (char c in possibleDelimiter)
                {
                    string[] parameter = s.Split(c);

                    if (parameter.Length == 2)
                    {
                        arguments.Add(parameter[0].Replace(@"\", string.Empty).Replace(@"/", string.Empty), parameter[1].Replace(@"""", ""));
                        break;
                    }
                }
            }

            ValidateArguments();
            //PrintArguments();

            ProcessWorkflow();
        }

        /// <summary>
        /// Validates the arguments.
        /// </summary>
        private void ValidateArguments()
        {
            //validate the argument keys
            if (!arguments.ContainsKey(ManifestArguments.Version.ToString()))
                throw new ArgumentNullException("version", "version key is requried");

            if (!arguments.ContainsKey(ManifestArguments.Template.ToString()))
                throw new ArgumentNullException("template", "manifest template name key is requried");

            if (!arguments.ContainsKey(ManifestArguments.Output.ToString()))
                throw new ArgumentNullException("output", "manifest output path key is requried");

            if (!arguments.ContainsKey(ManifestArguments.Tag.ToString()))
                throw new ArgumentNullException("tag", "tag key is requried");

            //validate the argument values
            if (string.IsNullOrWhiteSpace(arguments[ManifestArguments.Version.ToString()]))
                throw new ArgumentNullException("version", "version is requried");

            if (string.IsNullOrWhiteSpace(arguments[ManifestArguments.Template.ToString()]))
                throw new ArgumentNullException("template", "manifest template name is requried");

            if (string.IsNullOrWhiteSpace(arguments[ManifestArguments.Output.ToString()]))
                throw new ArgumentNullException("output", "manifest output path is requried");

            if (string.IsNullOrWhiteSpace(arguments[ManifestArguments.Tag.ToString()]))
                throw new ArgumentNullException("tag", "tag is requried");
        }

        /// <summary>
        /// Processes the workflow.
        /// </summary>
        private void ProcessWorkflow()
        {
            Dictionary<string, object> wfarguments = new Dictionary<string, object>();
            wfarguments["version"] = arguments[ManifestArguments.Version.ToString()];
            wfarguments["templateCategory"] = arguments[ManifestArguments.Template.ToString()];
            wfarguments["regions"] = arguments.ContainsKey(ManifestArguments.Regions.ToString()) ? arguments[ManifestArguments.Regions.ToString()] : null;
            wfarguments["outputManifestPath"] = arguments[ManifestArguments.Output.ToString()];
            wfarguments["tag"] = arguments[ManifestArguments.Tag.ToString()];
            wfarguments["searchDirectoryPath"] = arguments[ManifestArguments.SearchDirectoryPath.ToString()];
            var wf = new InvokeManifestAutomation();

            AsyncWorkflow(wf, wfarguments);
        }

        /// <summary>
        /// Asycs the workflow.
        /// </summary>
        /// <param name="wfarguments">The wfarguments.</param>
        private void AsyncWorkflow(Activity wf, Dictionary<string, object> wfarguments)
        {
            waitHandle = new AutoResetEvent(false);
            var wfa = new WorkflowApplication(wf, wfarguments);

            if (arguments[ManifestArguments.Template.ToString()].ToString().Equals(ManifestType.AppStore.ToString(), StringComparison.InvariantCultureIgnoreCase))
                wfa.Extensions.Add(new Manifest.DefaultImpl.AppStoreManifestFromTemplate());
            if (arguments[ManifestArguments.Template.ToString()].ToString().Equals(ManifestType.OMS.ToString(), StringComparison.InvariantCultureIgnoreCase))
                wfa.Extensions.Add(new Manifest.DefaultImpl.OMSManifestFromTemplate());
            if (arguments[ManifestArguments.Template.ToString()].ToString().Equals(ManifestType.Tibco.ToString(), StringComparison.InvariantCultureIgnoreCase))
                wfa.Extensions.Add(new Manifest.DefaultImpl.TibcoManifestFromTemplate());
            if (arguments[ManifestArguments.Template.ToString()].ToString().Equals(ManifestType.InStore.ToString(), StringComparison.InvariantCultureIgnoreCase))
                wfa.Extensions.Add(new Manifest.DefaultImpl.InStoreManifestFromTemplate());

            wfa.Completed = e =>
            {
                try
                {
                    // Did the workflow complete without error?
                    if (e.CompletionState == ActivityInstanceState.Closed)
                    {
                        try
                        {
                            Console.WriteLine(e.Outputs["state"]);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    if (e.CompletionState == ActivityInstanceState.Faulted)
                    {
                        throw e.TerminationException;
                    }
                }
                catch (Exception ex)
                {
                    wfException = ex;
                }
                finally
                {
                    // Must be sure to unblock the main thread
                    waitHandle.Set();
                }
            };

            wfa.Run();

            waitHandle.WaitOne();

            // Show the exception from the background thread
            if (wfException != null)
                throw new Exception(string.Format("WorkflowApplication error {0}", wfException.Message), wfException);
        }

        /// <summary>
        /// Syncs the workflow.
        /// </summary>
        /// <param name="wf">The wf.</param>
        /// <param name="wfarguments">The wfarguments.</param>
        private void SyncWorkflow(Activity wf, Dictionary<string, object> wfarguments)
        {
            WorkflowInvoker invoker = new WorkflowInvoker(wf);
            if (arguments[ManifestArguments.Template.ToString()].ToString().Equals(ManifestType.AppStore.ToString(), StringComparison.InvariantCultureIgnoreCase))
                invoker.Extensions.Add(new Manifest.DefaultImpl.AppStoreManifestFromTemplate());
            if (arguments[ManifestArguments.Template.ToString()].ToString().Equals(ManifestType.OMS.ToString(), StringComparison.InvariantCultureIgnoreCase))
                invoker.Extensions.Add(new Manifest.DefaultImpl.OMSManifestFromTemplate());
            if (arguments[ManifestArguments.Template.ToString()].ToString().Equals(ManifestType.Tibco.ToString(), StringComparison.InvariantCultureIgnoreCase))
                invoker.Extensions.Add(new Manifest.DefaultImpl.TibcoManifestFromTemplate());
            if (arguments[ManifestArguments.Template.ToString()].ToString().Equals(ManifestType.InStore.ToString(), StringComparison.InvariantCultureIgnoreCase))
                invoker.Extensions.Add(new Manifest.DefaultImpl.InStoreManifestFromTemplate());

            invoker.Invoke(wfarguments);

            Console.WriteLine("Working generated manifest successfully");
        }

        /// <summary>
        /// Prints the arguments.
        /// </summary>
        private void PrintArguments()
        {
            foreach (string d in arguments.Keys)
            {
                Console.WriteLine(string.Format("Argument Key:{0}, Argument value:{1}", d, arguments[d]));
            }
        }
    }
}
