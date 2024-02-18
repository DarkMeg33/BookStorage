let filepond;

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
            let files = filepond.getFiles();
            formData.append('bookCoverImage', files[0].file);

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
            },
        }
    });
}

function registerFilepond() {
    //$.fn.filepond.registerPlugin(
    //    FilePondPluginFileValidateSize,
    //    FilePondPluginImagePreview,
    //    FilePondPluginFileValidateType);

    //$.fn.filepond.setDefaults({
    //    maxFileSize: '3MB',
    //});

    //filepond = $('#filepond').filepond();

    FilePond.registerPlugin(
        FilePondPluginFileValidateType,
        FilePondPluginImagePreview,
        FilePondPluginFileValidateSize
    );

    filepond = FilePond.create(
        document.getElementById('filepond'),
        {
            maxFileSize: _get('bookCoverMaxSizeInMb'),
        }
    );
}

$(function () {
    bindForms();
    setupFormValidation();
    registerFilepond();
});
