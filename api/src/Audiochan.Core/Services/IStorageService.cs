﻿using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Audiochan.Core.Services
{
    public interface IStorageService
    {
        string CreatePutPresignedUrl(string bucket, string container, string blobName, int expirationInMinutes,
            Dictionary<string, string>? metadata = null);

        Task RemoveAsync(string bucket, string container, string blobName,
            CancellationToken cancellationToken = default);
        
        Task SaveAsync(Stream stream, string bucket, string container, string blobName,
            Dictionary<string, string>? metadata = null, CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(string bucket, string container, string blobName,
            CancellationToken cancellationToken = default);

        Task CopyBlobAsync(string sourceBucket,
            string sourceContainer,
            string sourceBlobName,
            string targetBucket,
            string targetContainer, 
            string? targetKey = null,
            CancellationToken cancellationToken = default);
        
        Task MoveBlobAsync(string sourceBucket,
            string sourceContainer,
            string sourceBlobName,
            string targetBucket,
            string targetContainer, 
            string? targetKey = null,
            CancellationToken cancellationToken = default);
    }
}