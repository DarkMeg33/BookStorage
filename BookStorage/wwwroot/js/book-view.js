let commentApp;
let chapterApp;

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
    $('#chapter-app').accordion();
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

    chapterApp = new Vue({
        el: '#chapter-app',
        data: {
            chapters: _get('chapters')
        },
        mounted: function () {
            setupChaptersAccordion();
        },
        methods: {
            goToChapterView: function(bookId, chapterId) {
                goTo(`/book/${bookId}/chapter/${chapterId}/view`);
            }
        }
    });

});
