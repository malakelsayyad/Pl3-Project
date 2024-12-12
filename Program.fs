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


