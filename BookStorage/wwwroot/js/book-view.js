let commentApp;

function bindForms() {
    bindFormSubmit({
        btnId: 'comment-btn',
        formId: 'comment-form',
        methodFn: () => {
            return 'post';
        },
        urlFn: () => {
            return `/book/${commentApp.bookId}/comment`;
        },
        onSuccessFn: (response) => {
            if (response.data) {
                commentApp.comments.unshift(response.data);
            }

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

function setupChaptersAccordion() {
    $('#chaptersAccordion').accordion();
}

$(function () {
    commentApp = new Vue({
        el: '#comment-app',
        data: {
            bookId: _get('bookId'),
            comments: _get('comments'),
        },
        mounted: function () {
            bindForms();
            setupFormValidation();
        },
        methods: {}
    });

    setupChaptersAccordion();
});
