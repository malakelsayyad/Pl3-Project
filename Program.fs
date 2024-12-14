open System
open System.Windows.Forms
open System.Drawing
open System.IO
open Newtonsoft.Json

// Dictionary storage
let mutable dictionary = Map.empty<string, string>

// File path for dictionary storage
let dictionaryFile = "dictionary.json"

// Helper function to normalize keys (trim and lowercase)
let normalizeKey (key: string) = key.Trim().ToLower()

// Save the dictionary to a JSON file
let saveDictionaryToFile () =
    let json = JsonConvert.SerializeObject(dictionary)
    File.WriteAllText(dictionaryFile, json)
    MessageBox.Show("Dictionary saved!") |> ignore

// Load the dictionary from a JSON file
let loadDictionaryFromFile () =
    if File.Exists(dictionaryFile) then
        try
            let json = File.ReadAllText(dictionaryFile)
            dictionary <- JsonConvert.DeserializeObject<Map<string, string>>(json)
            MessageBox.Show($"Dictionary loaded. Items count: {dictionary.Count}") |> ignore
        with
        | :? JsonSerializationException -> 
            MessageBox.Show("Error loading dictionary file. It may be corrupted.") |> ignore
            dictionary <- Map.empty<string, string>
    else
        MessageBox.Show("No dictionary file found.") |> ignore

// Dictionary operations
let addWord word definition =
    let normalizedWord = normalizeKey word
    dictionary <- dictionary.Add(normalizedWord, definition)


let addWord word definition =
    let normalizedWord = normalizeKey word
    dictionary <- dictionary.Add(normalizedWord, definition)

let updateWord word definition =
    let normalizedWord = normalizeKey word
    if dictionary.ContainsKey(normalizedWord) then
        dictionary <- dictionary.Add(normalizedWord, definition)
        true
    else
        false

let deleteWord word =
    let normalizedWord = normalizeKey word
    if dictionary.ContainsKey(normalizedWord) then
        dictionary <- dictionary.Remove(normalizedWord)
        true
    else
        false

let searchWord word =
    let normalizedWord = normalizeKey word
    dictionary.TryFind(normalizedWord)

let listAllWords () =
    dictionary |> Map.toList
let createMainForm () =
    let form = new Form(Text = "Dictionary", Width = 700, Height = 600, BackColor = Color.LightSteelBlue)

    // Main Layout
    let mainLayout = new TableLayoutPanel(Dock = DockStyle.Fill, RowCount = 2, ColumnCount = 1)
    mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40.0f)) // Input & buttons
    mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 60.0f)) // List view

    // Top Panel Layout
    let topPanel = new TableLayoutPanel(Dock = DockStyle.Fill, RowCount = 3, ColumnCount = 2)
    topPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30.0f)) // Labels
    topPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70.0f)) // Textboxes

    // Labels and Textboxes
    let lblWord = new Label(Text = "Word:", AutoSize = true, Font = new Font("Arial", 12.0f), ForeColor = Color.Black)
    let txtWord = new TextBox(Dock = DockStyle.Fill, Font = new Font("Arial", 12.0f))
    let lblDefinition = new Label(Text = "Definition:", AutoSize = true, Font = new Font("Arial", 12.0f), ForeColor = Color.Black)
    let txtDefinition = new TextBox(Dock = DockStyle.Fill, Font = new Font("Arial", 12.0f))

    topPanel.Controls.Add(lblWord, 0, 0)
    topPanel.Controls.Add(txtWord, 1, 0)
    topPanel.Controls.Add(lblDefinition, 0, 1)
    topPanel.Controls.Add(txtDefinition, 1, 1)

    // Add Top Panel to Main Layout
    mainLayout.Controls.Add(topPanel, 0, 0)

    // Add Main Layout to Form
    form.Controls.Add(mainLayout)