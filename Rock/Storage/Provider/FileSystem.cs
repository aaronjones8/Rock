﻿// <copyright>
// Copyright 2013 by the Spark Development Network
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//
using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO;

using Rock.Data;
using Rock.Model;

namespace Rock.Storage.Provider
{
    /// <summary>
    /// Storage provider for saving binary files to file system
    /// </summary>
    [Description( "File System-driven file storage" )]
    [Export( typeof( ProviderComponent ) )]
    [ExportMetadata( "ComponentName", "FileSystem" )]
    public class FileSystem : ProviderComponent
    {
        /// <summary>
        /// Saves the binary file contents to the external storage medium associated with the provider.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="context">The context.</param>
        /// <exception cref="System.ArgumentException">File Data must not be null.</exception>
        public override void SaveContent( BinaryFile binaryFile )
        {
            var filePath = GetFilePath( binaryFile );

            // Create the directory if it doesn't exist
            var directoryName = Path.GetDirectoryName( filePath );
            if ( !Directory.Exists( directoryName ) )
            {
                Directory.CreateDirectory( directoryName );
            }

            // Write the contents to file
            File.WriteAllBytes( filePath, binaryFile.Content );
        }

        /// <summary>
        /// Deletes the content from the external storage medium associated with the provider.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="context">The context.</param>
        public override void DeleteContent( BinaryFile binaryFile )
        {
            var file = new FileInfo( GetFilePath( binaryFile ) );
            if ( file.Exists )
            {
                file.Delete();
            }
        }

        /// <summary>
        /// Gets the contents from the external storage medium associated with the provider
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override Stream GetContentStream( BinaryFile binaryFile )
        {
            var file = new FileInfo( GetFilePath( binaryFile ) );
            if ( file.Exists )
            {
                return file.OpenRead();
            }

            return new MemoryStream();
        }

        /// <summary>
        /// Gets the file path.
        /// </summary>
        /// <param name="binaryFile">The binary file.</param>
        /// <returns></returns>
        private string GetFilePath( BinaryFile binaryFile )
        {
            if ( binaryFile != null && !string.IsNullOrWhiteSpace( binaryFile.FileName ) )
            {
                // If file's guid hasn't been set, set it now since it is required for filename
                if ( binaryFile.Guid.IsEmpty() )
                {
                    binaryFile.Guid = Guid.NewGuid();
                }

                string subFolder = string.Empty;

                var binaryFileType = binaryFile.BinaryFileType;
                if ( binaryFileType == null && binaryFile.BinaryFileTypeId.HasValue )
                {
                    binaryFileType = new BinaryFileTypeService( new RockContext() ).Get( binaryFile.BinaryFileTypeId.Value );
                }

                if ( binaryFileType != null )
                {
                    binaryFileType.LoadAttributes();
                    subFolder = binaryFileType.GetAttributeValue( "RootPath" );
                }

                if ( string.IsNullOrWhiteSpace( subFolder ) )
                {
                    subFolder = @"App_Data\Files";
                }
                else
                {
                    subFolder = subFolder.Replace( "/", @"\" );
                    subFolder = subFolder.TrimStart( "~".ToCharArray() );
                    subFolder = subFolder.TrimStart( @"\".ToCharArray() );
                }

                string folder = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, subFolder );
                string fileName = string.Format( "{0}_{1}", binaryFile.Guid, binaryFile.FileName );
                return Path.Combine( folder, fileName );
            }

            return string.Empty;
        }


    }
}
