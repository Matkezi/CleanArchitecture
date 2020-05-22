import React from 'react';

export class FileStorageHelper {
    static formatBase64Data = (data: string): string => {
        var startIndex = 0;
        var endIndex = data.indexOf(',');
        var base64Data = data.replace(data.substring(startIndex, endIndex + 1), "");
        return base64Data;
    }
}