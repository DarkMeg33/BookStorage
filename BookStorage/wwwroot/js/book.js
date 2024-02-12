let commentApp;

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
    $.fn.filepond.registerPlugin(
        FilePondPluginFileValidateSize,
        FilePondPluginImagePreview,
        FilePondPluginFileValidateType);

    $.fn.filepond.setDefaults({
        maxFileSize: '3MB',
    });

    $('#filepond').filepond();

    //FilePond.registerPlugin(
    //    FilePondPluginFileValidateType,
    //    FilePondPluginImageExifOrientation,
    //    FilePondPluginImagePreview,
    //    FilePondPluginImageCrop,
    //    FilePondPluginImageResize,
    //    FilePondPluginImageTransform,
    //    FilePondPluginImageEdit
    //);
    //FilePond.create(
    //    document.getElementById('filepond'),
    //    {
    //        labelIdle: `Drag & Drop your picture or <span class="filepond--label-action">Browse</span>`,
    //        imagePreviewHeight: 170,
    //        imageCropAspectRatio: '1:1',
    //        imageResizeTargetWidth: 200,
    //        imageResizeTargetHeight: 200,
    //        stylePanelLayout: 'compact circle',
    //        styleLoadIndicatorPosition: 'center bottom',
    //        styleProgressIndicatorPosition: 'right bottom',
    //        styleButtonRemoveItemPosition: 'left bottom',
    //        styleButtonProcessItemPosition: 'right bottom',
    //    }
    //);
}

$(function () {
    bindForms();
    setupFormValidation();
    registerFilepond();
});
