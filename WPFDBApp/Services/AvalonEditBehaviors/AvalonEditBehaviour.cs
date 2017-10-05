using ICSharpCode.AvalonEdit;
using System;
using System.Windows;
using System.Windows.Interactivity;

namespace WPFDBApp.Services.AvalonEditBehaviors
{
    public sealed class AvalonEditBehaviour : Behavior<TextEditor>
    {
        public static readonly DependencyProperty ScriptTextProperty =
            DependencyProperty.Register("ScriptText", typeof(string), typeof(AvalonEditBehaviour),
            new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, PropertyChangedCallback));

        public string ScriptText
        {
            get { return (string)GetValue(ScriptTextProperty); }
            set { SetValue(ScriptTextProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            if (AssociatedObject != null)
                AssociatedObject.TextChanged += AssociatedObjectOnTextChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (AssociatedObject != null)
                AssociatedObject.TextChanged -= AssociatedObjectOnTextChanged;
        }

        private void AssociatedObjectOnTextChanged(object sender, EventArgs eventArgs)
        {
            var textEditor = sender as TextEditor;
            if (textEditor != null)
            {
                if (textEditor.Document != null)
                    ScriptText = textEditor.Document.Text;
            }
        }

        private static void PropertyChangedCallback(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var behavior = dependencyObject as AvalonEditBehaviour;
            if (behavior.AssociatedObject != null)
            {
                var editor = behavior.AssociatedObject as TextEditor;
                if (editor.Document != null)
                {
                    try
                    {
                        if (editor.Document.Text != dependencyPropertyChangedEventArgs.NewValue.ToString())
                        {
                            var caretOffset = editor.CaretOffset;
                            editor.Document.Text = dependencyPropertyChangedEventArgs.NewValue.ToString();
                            editor.CaretOffset = Math.Min(editor.Document.TextLength, caretOffset);
                        }
                    }
                    catch { editor.Document.Text = String.Empty;}
                }
            }
        }
    }
}
