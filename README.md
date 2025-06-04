# Steganography
A simple C# application that hides an image inside a video file using a custom access code. The image is appended to the video without affecting playback, and can be extracted only by providing the correct code. This demonstrates a basic form of steganography and file manipulation.
# 🔐 C# File Hider (Image in Video Steganography)

This is a simple C# desktop application that hides an image file inside a video file using a secret code. The image is appended to the video file without affecting its playback. Later, the image can be extracted only if the correct code is provided.

---

## ✨ Features

- ✅ Append an image to the end of a video file
- 🔐 Protect the hidden image with a secret access code
- 🎥 Original video remains playable
- 📤 Extract the image only with the correct code
- 🧪 Simple demonstration of basic steganography using C#

---

## 🛠 How It Works

1. **Embedding**  
   The program appends a secret marker + code to the end of the video file, followed by the binary content of the image.

2. **Extraction**  
   The user enters the code. If it matches, the image is extracted from the combined file.

---

## 🚀 How to Run

### Requirements:
- .NET Framework or .NET Core
- Visual Studio or any C# IDE


// Provide your video path, image path, and secret code
