$(function () {

    var $hand = $('.hand');

    $hand.click(function () {
        var data = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
        data = data[Math.floor(Math.random() * data.length)];
        switch (data) {
            case 1:
                rotateFunc(1, 16, '恭喜你抽中了1个月绿钻');
                break;
            case 2:
                rotateFunc(2, 47, '恭喜你抽中了2个月绿钻');
                break;
            case 3:
                rotateFunc(3, 76, '恭喜你抽中了3个月绿钻');
                break;
            case 4:
                rotateFunc(4, 106, '恭喜你抽中了4个月绿钻');
                break;
            case 5:
                rotateFunc(5, 135, '恭喜你抽中了5个月绿钻');
                break;
            case 6:
                rotateFunc(6, 164, '恭喜你抽中了6个月绿钻');
                break;
            case 7:
                rotateFunc(7, 193, '恭喜你抽中了7个月绿钻');
                break;
            case 8:
                rotateFunc(7, 223, '恭喜你抽中了8个月绿钻');
                break;
            case 9:
                rotateFunc(7, 252, '恭喜你抽中了9个月绿钻');
                break;
            case 10:
                rotateFunc(7, 284, '恭喜你抽中了10个月绿钻');
                break;
            case 11:
                rotateFunc(7, 314, '恭喜你抽中了11个月绿钻');
                break;
            case 12:
                rotateFunc(7, 345, '恭喜你抽中了12个月绿钻');
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