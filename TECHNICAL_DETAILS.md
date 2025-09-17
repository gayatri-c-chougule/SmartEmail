# 🏗️ Architecture Description

## 📬 Keyword-based Email Downloader (C# / MailKit)
- Connects securely to IMAP servers (Gmail, Outlook, Yahoo, Zoho, Rediffmail and others as well).  
- Downloads emails matching by **date** (optionally filter by **subject keywords**), stored **month-wise**.  
- Saves raw messages as `.eml` files in a local folder.  

## ✉️ MIME Parsing & Metadata Extraction (C# + Python)
- **C# (MimeKit):** Handles email headers, sender, subject.  
- **Python (mailparser):** Extracts body text and decodes supported attachments (`.pdf`, `.docx`, `.txt`).  

## 🧠 Embedding Generation (Python, SentenceTransformers)
- Combines **subject + body + attachment text**.  
- Generates dense vector embeddings using **all-MiniLM-L6-v2**.  
- Produces `temp.json` containing embeddings + metadata.  

## 💾 Storage Layer (SQLite Database)
- Stores raw emails, metadata, and embeddings.  
- Provides **query embeddings** and **email embeddings** for similarity matching.  

## 🖥️ WinForms Desktop UI (C#)
- **Settings tab:** Configure provider, email path, database path.  
- **Download tab:** Retrieve & save emails.  
- **Embed tab:** Call Python scripts to generate embeddings.  
- **Finder tab:** Input a query → compute embedding → compare with stored vectors.  
- **Data Viewer tab:** Inspect database contents.  

## 🔎 Semantic Search (C# + SQLite)
- Query embeddings generated via Python script.  
- Cosine similarity computed in C# (`BaseFunctions.GetCosineSimilarity` `105_Functions`).  
- Top-K most relevant emails retrieved and displayed.  

## 📊 Result Viewer & Evaluation
- Finder tab displays results ranked by similarity.  
- Evaluation metrics (e.g., **Recall@K**) validate retrieval quality.  

---

# 📑 Application Pages

- **Settings** — Configure email account, enter app-specific password, and select email provider.  
- **Download** — Fetch emails via IMAP and save them as `.eml`. Includes a **date picker** and **filter words tab**. A **tree view** organizes downloaded emails month-wise.  
- **Embed** — Generate embeddings for stored emails. Any **unembedded emails** are highlighted in red.  
- **Query Management** — Add and embed new queries, or update/delete existing ones. A **tree view lists queries by short names**. Selecting a query repopulates the textboxes with its name and description.  
- **Finder** — Run semantic queries against the database. Results list all matching emails in **descending order of relevance**, showing **score, UID, and subject**.  
- **Data Viewer** — Inspect database tables and embeddings (both emails and queries).  
