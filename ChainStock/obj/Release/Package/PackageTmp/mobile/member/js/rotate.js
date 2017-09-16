$(function () {

    var $hand = $('.hand');

    $hand.click(function () {
        var data = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
        data = data[Math.floor(Math.random() * data.length)];
        switch (data) {
            case 1:
                rotateFunc(1, 16, '��ϲ�������1��������');
                break;
            case 2:
                rotateFunc(2, 47, '��ϲ�������2��������');
                break;
            case 3:
                rotateFunc(3, 76, '��ϲ�������3��������');
                break;
            case 4:
                rotateFunc(4, 106, '��ϲ�������4��������');
                break;
            case 5:
                rotateFunc(5, 135, '��ϲ�������5��������');
                break;
            case 6:
                rotateFunc(6, 164, '��ϲ�������6��������');
                break;
            case 7:
                rotateFunc(7, 193, '��ϲ�������7��������');
                break;
            case 8:
                rotateFunc(7, 223, '��ϲ�������8��������');
                break;
            case 9:
                rotateFunc(7, 252, '��ϲ�������9��������');
                break;
            case 10:
                rotateFunc(7, 284, '��ϲ�������10��������');
                break;
            case 11:
                rotateFunc(7, 314, '��ϲ�������11��������');
                break;
            case 12:
                rotateFunc(7, 345, '��ϲ�������12��������');
                break;
        }
    });

    var rotateFunc = function (awards, angle, text) {
        $hand.stopRotate();
        $hand.rotate({
            angle: 0,
            duration: 5000,
            animateTo: angle + 1440,
            callback: function () {
                alert(text);
            }
        });
    };
});