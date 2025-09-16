import sys
import json
from sentence_transformers import SentenceTransformer

# Load the SentenceTransformer model once at the start
Model = SentenceTransformer('all-MiniLM-L6-v2')

def main():
    """
    Command-line tool that accepts a string input and returns its sentence embedding
    using the all-MiniLM-L6-v2 model. The embedding is printed as a JSON array.

    Usage:
        python embed_text.py "Your input text here"
    """
    # Check if input string is provided as the second argument
    if len(sys.argv) < 2:
        print("Error: No input string provided.", file=sys.stderr)
        sys.exit(1)  # Exit with failure code

    inputText = sys.argv[1]

    # Generate sentence embedding and convert to JSON-serializable list
    embedding = Model.encode(inputText).tolist()

    # Output embedding as a JSON array to stdout
    print(json.dumps(embedding))

if __name__ == "__main__":
    main()
