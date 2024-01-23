function bindForms() {
    bindFormSubmit({
        btnId: 'comment-btn',
        formId: 'comment-form',
        methodFn: () => {
            return 'post';
        },
        urlFn: () => {
            return `/book/${bookId}/comment`;
        },
        onSuccessFn: (response) => {
            showSuccess('Success');
        }
    });
}

function setupFormValidation() {
    $('#comment-form').form({
        fields: {
            text: {
                identifier: 'text',
                rules: [
                    {
                        type: 'empty',
                        prompt: 'Input should be populated'
                    }
                ]
            },
        }
    });
}

$(function () {
    bindForms();
    setupFormValidation();
});
