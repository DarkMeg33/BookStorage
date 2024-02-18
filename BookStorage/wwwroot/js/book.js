let bookCoverFilepond;
let bookFileFilepond;

function bindForms() {
    bindFormSubmit({
        btnId: 'create-book-btn',
        formId: 'book-form',
        methodFn: () => {
            return 'post';
        },
        urlFn: () => {
            return `/book`;
        },
        onSuccessFn: (response) => {
            showSuccess('Success');

            if (response.data) {
                goTo(`/book/${response.data.bookId}/view`)
            }
        },
        formDataBeforeSubmitFn: (formData)  => {
            let bookCoverFiles = bookCoverFilepond.getFiles();
            if (!!bookCoverFiles[0]) {
                formData.append('bookCoverImage', bookCoverFiles[0].file);
            }

            let bookFiles = bookFileFilepond.getFiles();
            if (!!bookFiles[0]) {
                formData.append('bookFile', bookFiles[0].file);
            }

            return formData;
        }
    });
}

function setupFormValidation() {
    $('#book-form').form({
        fields: {
            title: {
                identifier: 'title',
                rules: [
                    {
                        type: 'empty',
                        prompt: 'Title should be populated'
                    }
                ]
            },
            description: {
                identifier: 'description',
                rules: [
                    {
                        type: 'empty',
                        prompt: 'Description should be populated'
                    }
                ]
            }
        }
    });
}

function registerBookCoverFilepond() {
    FilePond.registerPlugin(
        FilePondPluginFileValidateType,
        FilePondPluginImagePreview,
        FilePondPluginFileValidateSize
    );

    bookCoverFilepond = FilePond.create(
        document.getElementById('book-cover'),
        {
            maxFileSize: _get('bookCoverMaxSizeInMb'),
        }
    );
}

function registerBookFileFilepond() {
    FilePond.registerPlugin(
        FilePondPluginFileValidateType,
        FilePondPluginFileValidateSize
    );

    bookFileFilepond = FilePond.create(
        document.getElementById('book-file'),
        {
            maxFileSize: _get('bookFileMaxSizeInMb'),
        }
    );
}

$(function () {
    bindForms();
    setupFormValidation();
    registerBookCoverFilepond();
    registerBookFileFilepond();
});
