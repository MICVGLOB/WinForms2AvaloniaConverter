# WinForms2AvaloniaConverter

WinForms2AvaloniaConverter helps you migrate a Windows Forms project to an Avalonia UI project. It can convert entire projects or individual files.

The converter migrates WinForms UI controls to Eremex Avalonia UI and standard Avalonia UI counterparts. You can customize the converter to change control mapping.

The converter provides the following project migration features:

- Uses specified control mapping rules to convert WinForms controls to Eremex Avalonia UI and standard Avalonia UI controls. 
- Separates definition of source Form and UserControl classes into View and View Model classes according to the MVVM design pattern. Creates corresponding files in the destination project/folder.
- Creates observable properties and commands in View Models (using the CommunityToolkit.Mvvm library), and binds generated Avalonia UI controls to them.
- Copies resources from the source project to the destination project/folder.
- Extracts images from resource files in the source project, and saves them to the destination project/folder.

The converter does not transfer business logic from code-behind, nor does it transfer code from additional _*.cs_ files in the source project. You need to copy this code manually to the Avalonia project.

## Online Video

- [WinForms2AvaloniaConverter](https://www.youtube.com/watch?v=XKk6p7CFUMQ) (Russian)

## Convert Projects or Individual Files

For small projects that consist of a few forms, you can use the converter to migrate the entire project. Small projects are easier to check after conversion.

Conversion of individual files is handy for large projects that consist of a multitude of forms. You can iteratively convert forms one by one: port a specific form(s), move the converted files to the target Avalonia UI project, check the result of conversion, and then continue converting other forms.

Conversion of individual files is also useful for testing the converter.

## Get Started with Project Conversion

1. Download and then open the WinForms2AvaloniaConverter project in Visual Studio.
2. Customize control mapping rules for Windows Forms controls used in your source project, as described in [Converting UI and Creating View Models](#converting-ui-and-creating-view-models).
3. Specify the target Avalonia UI framework version using the `XamlConverter.avaloniaVersion` property.
4. Specify the target version of the Eremex Avalonia UI controls using the `XamlConverter.controlsVersion` property.
5. Build the WinForms2AvaloniaConverter library, or create a NuGet package for the WinForms2AvaloniaConverter library.
6. Include the created library/NuGet package into your WinForms project that needs to be converted. Alternatively, you can include the source files of the WinForms2AvaloniaConverter library into your WinForms project.
7. In your source project, inherit all `System.Windows.Forms.Form` objects from the `WAConverter.WAForm` class, and inherit all `System.Windows.Forms.UserControl` objects from the `WAConverter.WAUserControl` class.
8. Run the project.
9. Open all forms one by one at runtime, so the converter can analyze them. The converter recursively iterates through the Controls collection of each opened Form, and collects information about the names, position, size of the controls and their properties. After data is collected, it generates Avalonia UI files in the destination folder (see `./Bin/../Converted`).
10. Copy code that was skipped during conversion to the destination project.
11. Test the project.
   

## Converting UI and Creating View Models

### Views

For source WinForms Form and UserControl classes, the converter generates View classes in the _*.axaml_ and _*.axaml.cs_ files. The names of the source forms and user controls determine the names of generated View classes and files.

WinForms controls nested in source forms and user controls are converted to Eremex Avalonia UI and standard Avalonia UI counterparts by default. You can modify the `XamlConverter` class to adapt the conversion rules to your WinForms project.

#### Related API

- `XamlConverter.typesMapping` dictionary (initialized in the `XamlConverter.InitTypeMapping` method) — Use this member to customize control mapping rules according to your requirements. 
- `XamlConverter.ignoredControls` property — Specifies a list of controls ignored during the conversion. For instance, this list contains the `HScrollBar` and `VScrollBar` controls, by default.
- `XamlConverter.ConvertControlCore` — Implements generation of XAML attributes for Avalonia UI controls.

Avalonia UI does not support control positioning using absolute coordinates. 
During conversion, the `System.Windows.Forms.TableLayoutPanel` container is converted to the `Avalonia.Controls.Grid` container. Controls positioned within `TableLayoutPanel` cells are placed within corresponding `Grid` cells. All other controls are combined in the `StackPanel` container.

### View Models

Beside Views, the converter creates View Models in _*.cs_ files for Forms and UserControls. The View Models define commands (`RelayCommand`) for clickable controls (buttons), and observable properties that should provide data for specific controls.

UI controls in View classes contain bindings to the commands and observable properties defined in View Models.

#### Related API

- `XamlConverter.GenerateFields` — Creates observable properties for specific Avalonia UI controls.
- `XamlConverter.GenerateMethods` — Generates commands in View Models.
- `XamlConverter.IsCommandControl` —  Specifies controls for which commands are generated.


## Processing and Copying Resources

The converter searches for resources (_&ast;.resx_ files) and localization resources (_&ast;.&lt;Localized&gt;.resx_ files) in the source project's directory, and then copies found files to the destination folder. 

When copying resource files, a cleanup function keeps only specific data properties (`Text` and `Caption`), and skips irrelevant properties (`Name`, `Parent`, `ZOrder`, and `Type`).

In WinForms, images are typically stored within _.resx_ files in binary format. The converter extracts these images from _.resx_ files and saves them as standalone image files in the destination folder.

#### Related API

- `XamlConverter.AddResXFiles` — Copies _*.resx_ files to the destination folder.
- `ResXCleaner.CleanupFile` — Extracts images from a _*.resx_ file in the destination folder and saves them as standalone image files. Removes irrelevant properties from the _*.resx_ file.
- `ResXCleaner.ignoredProperties` — Specifies a list of irrelevant properties removed by a cleanup function from _*.resx_ files in the destination folder.

## Customize Avalonia UI Project Template

The `AppTemplate` folder of the WinForms2AvaloniaConverter project contains files that define the template used to generate an Avalonia UI project. The template's files are stored as Embedded Resources. You can modify the template to suit your requirements.

#### Related API

- `XamlConverter.CreateAppFromTemplate` — Generates an Avalonia UI project from the project template.
