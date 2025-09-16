import os
import json
import mailparser
from sentence_transformers import SentenceTransformer
import sys

# ------------------------- Argument Parsing -------------------------

# Ensure script is run with required arguments: comma-separated UIDs and email folder path
if len(sys.argv) < 3:
    sys.exit(1)

uid_string = sys.argv[1]
UidsToBeEmbedded = set(uid.strip() for uid in uid_string.split(",") if uid.strip())
EmailFolder = sys.argv[2]
OutputJSON = os.path.join(EmailFolder, "temp.json")

# ------------------------- Model Initialization -------------------------

# Load the SentenceTransformer model for embedding emails
Model = SentenceTransformer('all-MiniLM-L6-v2')
EmbeddedEmails = []

# ------------------------- Utility Functions -------------------------

def Clean(text):
    """
    Strip leading/trailing spaces and normalize newlines from the given text.
    """
    return text.strip().replace('\n', ' ').replace('\r', ' ')

def ExtractEmailComponents(email_path):
    """
    Parse the given .eml file and extract components such as subject, body,
    sender, UID, date, and text content from selected attachments.
    
    Args:
        email_path (str): Full path to the .eml file.
    
    Returns:
        dict: Parsed email information including metadata and attachment content.
    """
    mail = mailparser.parse_from_file(email_path)

    subject = Clean(mail.subject or "")
    email_body = Clean(mail.body or "")
    sender = mail.from_[0][1] if mail.from_ else ""
    
    # UID is assumed to be in fixed position in filename: characters 16 to 20
    filename = os.path.basename(email_path)
    uid = filename[16:20]

    # Try to extract date in YYYYMMDD format
    try:
        xDate = mail.date
        date_yyyyMMdd = xDate.strftime("%Y%m%d")
    except Exception:
        date_yyyyMMdd = ""

    # Filter and decode selected attachment types
    allowedExts = {'.pdf', '.doc', '.docx', '.txt'}
    attachments = []

    for attachment in mail.attachments:
        fileName = attachment.get('filename', '').lower()
        if any(fileName.endswith(ext) for ext in allowedExts):
            attContentBytes = attachment.get('payload', b'')
            try:
                attContentText = attContentBytes.decode('utf-8', errors='ignore')
            except Exception:
                attContentText = ''
            attachments.append({
                'FileName': fileName,
                'Content': attContentText
            })

    return {
        "UID": uid,
        "Date_yyyyMMdd": date_yyyyMMdd,
        "Sender": sender,
        "Subject": subject,
        "EmailContent": email_body,
        "Attachments": attachments
    }

# ------------------------- Main Processing Function -------------------------

def process_emails():
    """
    Process all .eml files in the given folder matching specified UIDs.
    Extracts and embeds the email content using SentenceTransformer,
    including subject, body, and relevant attachments.
    Saves the result as JSON containing email metadata and embeddings.
    """
    for filename in os.listdir(EmailFolder):
        if filename.endswith(".eml") and filename[16:20] in UidsToBeEmbedded:
            full_path = os.path.join(EmailFolder, filename)
            email_data = ExtractEmailComponents(full_path)

            # Combine subject, body, and attachment contents for embedding
            fullEmailContent = f"{email_data['Subject']}\n{email_data['EmailContent']}"
            for attachment in email_data["Attachments"]:
                fullEmailContent += f"\n{attachment['Content']}"

            emailEmbedding = Model.encode(fullEmailContent).tolist()

            EmbeddedEmails.append({
                "UID": email_data["UID"],
                "Date_yyyyMMdd": email_data["Date_yyyyMMdd"],
                "Sender": email_data["Sender"],
                "Subject": email_data["Subject"],
                "EmailContent": email_data["EmailContent"],
                "Attachments": email_data["Attachments"],
                "Embedding": emailEmbedding
            })

    # Write output to JSON file
    with open(OutputJSON, "w") as f:
        json.dump(EmbeddedEmails, f, indent=2)

    print(f"✅ Saved {len(EmbeddedEmails)} email embeddings to {OutputJSON}")

# ------------------------- Entry Point -------------------------

if __name__ == "__main__":
    process_emails()
