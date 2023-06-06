export const convertToBase64 = (file: File | null): Promise<string> => {
  return new Promise((resolve, reject) => {
    if (!file) {
      reject(new Error("No file selected."));
      return;
    }

    const reader = new FileReader();

    reader.onload = (event: ProgressEvent<FileReader>) => {
      if (event.target?.result) {
        const base64String = event.target.result as string;
        resolve(base64String);
      } else {
        reject(new Error("Failed to convert the image to Base64."));
      }
    };

    reader.onerror = (event: ProgressEvent<FileReader>) => {
      reject(new Error("Failed to convert the image to Base64."));
    };

    reader.readAsDataURL(file);
  });
};
