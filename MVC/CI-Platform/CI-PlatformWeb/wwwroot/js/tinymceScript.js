tinymce.init({
    menubar: false,

    selector: 'textarea#tiny',

    plugins: [

        'a11ychecker', 'advlist', 'advcode', 'advtable', 'autolink', 'checklist', 'export',

        'lists', 'charmap', 'preview', 'anchor', 'searchreplace', 'visualblocks',

        'powerpaste', 'fullscreen', 'formatpainter', 'insertdatetime', 'wordcount'

    ],

    toolbar: 'undo redo | a11ycheck casechange blocks | bold italic | alignleft aligncenter alignright alignjustify |' +

        'bullist numlist checklist outdent indent | removeformat | code'

})