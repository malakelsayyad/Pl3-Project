Addbtn.Click.Add(fun _->
        if not(String.IsNullOrWhiteSpace(txtWord.Text) || String.IsNullOrWhiteSpace(txtDefinition.Text)) then
            addWord txtWord.Text txtDefinition.Text
            saveDictionaryToFile()
            MessageBox.Show($"Word '{txtWord.Text}' added successfully!") |> ignore
            txtWord.Clear()
            txtDefinition.Clear()
            dgvWords.Rows.Clear() // Clear previous rows
            for (word, definition) in listAllWords() do
    dgvWords.Rows.Add(word, definition) |> ignore
        else
    MessageBox.Show("Both word and definition are required!") |> ignore
    )

    Updatebtn.Click.Add(fun _->
        if updateWord txtWord.Text txtDefinition.Text then
            saveDictionaryToFile()
            MessageBox.Show($"Word '{txtWord.Text}' updated successfully!") |> ignore
            txtWord.Clear()
            txtDefinition.Clear()
            dgvWords.Rows.Clear() // Clear previous rows
            for (word, definition) in listAllWords () do
                dgvWords.Rows.Add(word, definition) |> ignore
        else
            MessageBox.Show($"Word '{txtWord.Text}' not found!") |> ignore
    )