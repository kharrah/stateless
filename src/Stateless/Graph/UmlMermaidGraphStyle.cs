using System.Collections.Generic;
using Stateless.Reflection;

namespace Stateless.Graph
{
    /// <summary>
    /// Generate MERMAID graphs in basic UML style
    /// </summary>
    public class UmlMermaidGraphStyle : GraphStyleBase
    {
        /// <summary>
        /// Returns the formatted text for a single superstate and its substates.
        /// For example, for DOT files this would be a subgraph containing nodes for all the substates.
        /// </summary>
        /// <param name="stateInfo">The superstate to generate text for</param>
        /// <returns>Description of the superstate, and all its substates, in the desired format</returns>
        public override string FormatOneCluster(SuperState stateInfo)
        {
            var stateRepresentationString = "\n";

            stateRepresentationString += $"\nstate {stateInfo.StateName} {{";

            foreach (var subState in stateInfo.SubStates)
            {
                stateRepresentationString += FormatOneState(subState);
            }

            stateRepresentationString += "}\n";

            return stateRepresentationString;
        }

        /// <summary>
        /// Generate the text for a single decision node
        /// </summary>
        /// <param name="nodeName">Name of the node</param>
        /// <param name="label">Label for the node</param>
        /// <returns></returns>
        public override string FormatOneDecisionNode(string nodeName, string label)
        {
            return "";
        }

        /// <summary>
        /// Generate the text for a single state
        /// </summary>
        /// <param name="state">The state to generate text for</param>
        /// <returns></returns>
        public override string FormatOneState(State state)
        {
            return $"state \"{state.StateName}\" as {state.StateName}\n";
        }

        /// <summary>
        /// Get the text that starts a new graph
        /// </summary>
        /// <returns></returns>
        public override string GetPrefix()
        {
            return "stateDiagram\n";
        }

        /// <summary>
        /// Get the text that ends a new graph
        /// </summary>
        /// <returns></returns>
        public override string GetSuffix()
        {
            return "";
        }

        /// <summary>
        /// Add initial transition
        /// </summary>
        /// <param name="initialState"></param>
        /// <returns></returns>
        public override string FormatInitialTransition(StateInfo initialState)
        {
            return "";
        }

        /// <summary>
        /// Generate text for a single transition
        /// </summary>
        /// <param name="sourceNodeName"></param>
        /// <param name="trigger"></param>
        /// <param name="actions"></param>
        /// <param name="destinationNodeName"></param>
        /// <param name="guards"></param>
        /// <returns></returns>
        public override string FormatOneTransition(string sourceNodeName, string trigger,
            IEnumerable<string> actions, string destinationNodeName, IEnumerable<string> guards)
        {
            var label = trigger ?? "";

            return FormatOneLine(sourceNodeName, destinationNodeName, label);
        }

        internal string FormatOneLine(string fromNodeName, string toNodeName, string label)
        {
            if (!string.IsNullOrWhiteSpace(label))
            {
                label = $" : {label}";
            }

            return $"{fromNodeName} --> {toNodeName}{label}";
        }
    }
}
