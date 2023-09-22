using Godot;
using System;
using System.Reflection;

namespace OnReadyCs
{
    // Custom attribute to specify that a field should be initialized with a node fetched by its path when the scene is ready.
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class OnReadyAttribute : Attribute
    {
        // Property to store the path of the node to be fetched.
        public string NodePath { get; }

        // Constructor to set the node path.
        public OnReadyAttribute(string nodePath)
        {
            NodePath = nodePath;
        }
    }

    // Extension methods for the Node class.
    public static class NodeExtensions
    {
        // Initializes fields in a Node instance that are marked with the OnReadyAttribute.
        public static void InitializeOnReadyFields(this Node instance)
        {
            // Get all instance fields of the node that are non-public (like private or protected).
            FieldInfo[] fields = instance.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            // Iterate over each field.
            foreach (FieldInfo field in fields)
            {
                // Check if the field has the OnReadyAttribute.
                if (Attribute.IsDefined(field, typeof(OnReadyAttribute)))
                {
                    // Get the attribute.
                    OnReadyAttribute attr = (OnReadyAttribute)Attribute.GetCustomAttribute(field, typeof(OnReadyAttribute));

                    // Fetch the node based on the path provided in the attribute.
                    Node node = instance.GetNode(attr.NodePath);
                    
                    // Set the value of the field to the fetched node.
                    field.SetValue(instance, node);
                }
            }

            // Get all instance properties of the node
            PropertyInfo[] properties = instance.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            // Iterate over each property.
            foreach (PropertyInfo property in properties)
            {
                // Check if the property has the OnReady attribute
                if (Attribute.IsDefined(property, typeof(OnReadyAttribute)))
                {
                    // Get the attribute.
                    OnReadyAttribute attr = (OnReadyAttribute)Attribute.GetCustomAttribute(property, typeof(OnReadyAttribute));

                    // Fetch the node based on the path provided in the attribute.
                    Node node = instance.GetNode(attr.NodePath);
                    
                    // Ensure the property has a setter
                    if (property.SetMethod != null)
                    {
                        property.SetValue(instance, node);
                    }
                    else
                    {
                        GD.PrintErr($"The property {property.Name} in {instance.GetType().Name} has an [OnReady] attribute but lacks a 'setter'.");
                    }
                }
            }
        }
    }
}
