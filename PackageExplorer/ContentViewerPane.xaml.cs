﻿using System.Collections.Generic;
using System.Windows.Controls;
using ICSharpCode.AvalonEdit.Highlighting;
using NuGetPackageExplorer.Types;

namespace PackageExplorer {
    /// <summary>
    /// Interaction logic for ContentViewerPane.xaml
    /// </summary>
    public partial class ContentViewerPane : UserControl {
        public ContentViewerPane() {
            InitializeComponent();
            PopulateLanguageBoxValues();

            // disable unnecessary editor features
            contentBox.Options.CutCopyWholeLine = false;
            contentBox.Options.EnableEmailHyperlinks = false;
            contentBox.Options.EnableHyperlinks = false;
        }

        private void PopulateLanguageBoxValues() {
            // set the Syntax Highlighting definitions
            var definitions = new List<IHighlightingDefinition>();
            definitions.Add(TextHighlightingDefinition.Instance);
            definitions.AddRange(HighlightingManager.Instance.HighlightingDefinitions);
            LanguageBox.ItemsSource = definitions;
        }

        private void UserControl_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e) {
            var info = (FileContentInfo)DataContext;
            if (info != null && info.IsTextFile) {
                LanguageBox.SelectedItem = FileUtility.DeduceHighligtingDefinition(info.File.Name);
                contentBox.Load(StreamUtility.ToStream((string)info.Content));
            }
        }
    }
}
