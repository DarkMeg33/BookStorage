let navigationBtnsApp;

function setupChaptersDropdown() {
    $('#chapters-nav-dropdown').dropdown({

    });
}

$(function () {
    setupChaptersDropdown();

    navigationBtnsApp = new Vue({
        el: '#nav-btns-app',
        data: {
            isFirstChapter: _get('isFirstChapter'),
            isLastChapter: _get('isLastChapter'),
            
        },
        mounted: function () {
        },
        methods: {
            goToChapter: function (bookId, chapterId) {
                goTo(`/book/${bookId}/chapter/${chapterId}/view`);
            }
        }
    });
});
